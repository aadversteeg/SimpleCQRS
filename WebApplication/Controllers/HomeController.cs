using System;
using System.Diagnostics;
using Application.Commands;
using Infrastructure;
using Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandSender _bus;
        private readonly IReadModelFacade _readmodel;

        public HomeController(ICommandSender bus, IReadModelFacade readmodel)
        {
            _bus = bus;
            _readmodel = readmodel;
        }

        public ActionResult Index()
        {
            var model = new Models.Home.Index
            {
                Items = _readmodel.GetInventoryItems()
            };
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var item = _readmodel.GetInventoryItemDetails(id);
            var model = new Models.Home.Details
            {
                Id = item.Id,
                Name = item.Name,
                Version = item.Version,
                CurrentCount = item.CurrentCount
            };
            return View(model);
        }

        public ActionResult Add()
        {
            var model = new Models.Home.Add();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Models.Home.Add model)
        {
            _bus.Send(new CreateInventoryItem(Guid.NewGuid(), model.Name));
            return RedirectToAction("Index");
        }

        public ActionResult ChangeName(Guid id)
        {
            var item = _readmodel.GetInventoryItemDetails(id);
            var model = new Models.Home.ChangeName
            {
                Id = item.Id,
                Name = item.Name,
                Version = item.Version
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeName(Models.Home.ChangeName model)
        {
            var command = new RenameInventoryItem(model.Id, model.Name, model.Version);
            _bus.Send(command);

            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(Guid id)
        {
            var item = _readmodel.GetInventoryItemDetails(id);
            var model = new Models.Home.Deactivate
            {
                Id = item.Id,
                Version = item.Version
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Deactivate(Models.Home.Deactivate model)
        {
            _bus.Send(new DeactivateInventoryItem(model.Id, model.Version));
            return RedirectToAction("Index");
        }

        public ActionResult CheckIn(Guid id)
        {
            var item = _readmodel.GetInventoryItemDetails(id);
            var model = new Models.Home.CheckIn
            {
                Id = item.Id,
                Version = item.Version,
                Number = 0
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckIn(Models.Home.CheckIn model)
        {
            _bus.Send(new CheckInItemsToInventory(model.Id, model.Number, model.Version));
            return RedirectToAction("Index");
        }

        public ActionResult Remove(Guid id)
        {
            var item = _readmodel.GetInventoryItemDetails(id);
            var model = new Models.Home.Remove
            {
                Id = item.Id,
                Version = item.Version
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Remove(Models.Home.Remove model)
        {
            _bus.Send(new RemoveItemsFromInventory(model.Id, model.Count, model.Version));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
