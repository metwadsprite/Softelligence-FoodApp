using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Abstractions;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class StoreManagementController : Controller
    {

        private AdminService adminService;
        public StoreManagementController(AdminService adminService)
        {
            this.adminService = adminService;

        }
        public IActionResult Index()
        {
            ViewBag.ViewName = "StoreManagement";
            var storesList = adminService.GetAllStores();
            return View(storesList);
        }

        [HttpPost]
        public IActionResult Add([FromForm]Store newStore)
        {
            if(ModelState.IsValid)
            {               
                adminService.AddStore(newStore);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            var store = new Store();
            store.Menu = new Menu();
            return View(store);
        }

        public IActionResult Details(int? id)
        {
            Store store = new Store();
            var storeRepo = adminService.GetAllStores();
            foreach(var item in storeRepo)
            {
                if(item.Id == id)
                {
                    store = item;
                }
            }
            return View(store);
        }

        public IActionResult Update(int? id)
        {
            Store store = new Store();
            var storeRepo = adminService.GetAllStores();
            foreach (var item in storeRepo)
            {
                if (item.Id == id)
                {
                    store = item;
                }
            }
            return View(store);
        }

    }
}