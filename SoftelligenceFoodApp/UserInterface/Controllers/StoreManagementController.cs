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
        public IActionResult Add([Bind("Id,Name,Link")] Store newStore)
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
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}