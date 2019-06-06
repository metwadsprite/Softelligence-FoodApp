using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.BusinessExceptions;
using Logic.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class StoreManagementController : Controller
    {
        private readonly AdminService adminService;

        public StoreManagementController(AdminService adminService)
        {
            this.adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.ViewName = "StoreManagement";
            var storesList = new List<StoreVM>();

            var storesToDisplay = adminService.GetAllStores();

            foreach (var store in storesToDisplay)
            {
                var storeToDisplay = new StoreVM();
                storeToDisplay.Id = store.Id;
                storeToDisplay.Name = store.Name;
                storeToDisplay.isActive = store.IsActive;
                if (store.Menu != null)
                {
                    if (store.Menu.Hyperlink != null)
                    {
                        storeToDisplay.Hyperlink = store.Menu.Hyperlink;
                    }
                    if (store.Menu.Image != null)
                    {
                        storeToDisplay.Image = store.Menu.Image;
                    }
                }

                storesList.Add(storeToDisplay);
            }

            return View(storesList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add([FromForm]StoreVM newStore)
        {
            if (ModelState.IsValid)
            {
                var storeToAdd = new Store
                {
                    Name = newStore.Name,
                    Menu = new Menu
                    {
                        Hyperlink = newStore.Hyperlink,
                        Image = newStore.Image
                    }
                };
                adminService.AddStore(storeToAdd);

            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var store = new StoreVM();
            return View(store);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            Store store = adminService.GetStoreById(id.Value);
            return View(store);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            Store store = adminService.GetStoreById(id.Value);
            return View(store);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update([FromForm]Store newStore)
        {
            if (ModelState.IsValid)
            {
                adminService.UpdateStore(newStore);
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Store store = adminService.GetStoreById(id.Value);

            return View(store);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete([FromForm]Store storeToRemove)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adminService.RemoveStore(storeToRemove);
                }
                catch(StoreIsActiveException)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}