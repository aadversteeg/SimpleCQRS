using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var bus = new Infrastructure.InMemory.FakeBus();

            var storage = new Infrastructure.InMemory.EventStore(bus);
            var rep = new Infrastructure.InMemory.Repository<Domain.InventoryItem>(storage);

            var commands = new Application.InventoryCommandHandlers(rep);
            bus.RegisterHandler<Application.Commands.CheckInItemsToInventory>(commands.Handle);
            bus.RegisterHandler<Application.Commands.CreateInventoryItem>(commands.Handle);
            bus.RegisterHandler<Application.Commands.DeactivateInventoryItem>(commands.Handle);
            bus.RegisterHandler<Application.Commands.RemoveItemsFromInventory>(commands.Handle);
            bus.RegisterHandler<Application.Commands.RenameInventoryItem>(commands.Handle);

            var detail = new Infrastructure.InMemory.InventoryItemDetailView();
            bus.RegisterHandler<Infrastructure.Events.InventoryItemCreated>(detail.Handle);
            bus.RegisterHandler<Infrastructure.Events.InventoryItemDeactivated>(detail.Handle);
            bus.RegisterHandler<Infrastructure.Events.InventoryItemRenamed>(detail.Handle);
            bus.RegisterHandler<Infrastructure.Events.ItemsCheckedInToInventory>(detail.Handle);
            bus.RegisterHandler<Infrastructure.Events.ItemsRemovedFromInventory>(detail.Handle);

            var list = new Infrastructure.InMemory.InventoryListView();
            bus.RegisterHandler<Infrastructure.Events.InventoryItemCreated>(list.Handle);
            bus.RegisterHandler<Infrastructure.Events.InventoryItemRenamed>(list.Handle);
            bus.RegisterHandler<Infrastructure.Events.InventoryItemDeactivated>(list.Handle);

            services.AddSingleton<Infrastructure.InMemory.ICommandSender, Infrastructure.InMemory.FakeBus>( s => bus);
            services.AddSingleton<Infrastructure.IReadModelFacade, Infrastructure.InMemory.ReadModelFacade>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
