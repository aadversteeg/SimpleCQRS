using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.Commands;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IReadModelFacade _readmodel;

        public HomeController(IMediator mediator, IReadModelFacade readmodel)
        {
            _mediator = mediator;
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
        public async Task<ActionResult> Add(Models.Home.Add model)
        {
            await _mediator.Send(new CreateInventoryItem(Guid.NewGuid(), model.Name));
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
        public async Task<ActionResult> ChangeName(Models.Home.ChangeName model)
        {
            var command = new RenameInventoryItem(model.Id, model.Name, model.Version);
            await _mediator.Send(command);

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
        public async Task<ActionResult> Deactivate(Models.Home.Deactivate model)
        {
            await _mediator.Send(new DeactivateInventoryItem(model.Id, model.Version));
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
        public async Task<ActionResult> CheckIn(Models.Home.CheckIn model)
        {
            await _mediator.Send(new CheckInItemsToInventory(model.Id, model.Number, model.Version));
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
        public async Task<ActionResult> Remove(Models.Home.Remove model)
        {
            await _mediator.Send(new RemoveItemsFromInventory(model.Id, model.Count, model.Version));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
