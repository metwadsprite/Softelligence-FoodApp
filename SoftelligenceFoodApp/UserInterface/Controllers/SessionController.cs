using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class SessionController : Controller
    {
        private AdminService adminService;

        public SessionController(AdminService adminService)
        {
            this.adminService = adminService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            ViewBag.ViewName = "Session";
            var sessionList = adminService.GetAllSessions();
            return View(sessionList);
           
        }
        public IActionResult NewSession()
        {
            ViewBag.ViewName = "Session";
            var storesList = adminService.GetAllStores();
            return View(storesList);
        }

    }
}