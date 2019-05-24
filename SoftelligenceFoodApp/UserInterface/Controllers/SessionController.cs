using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

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
            var sessionsList = adminService.GetAllSessions();
            return View(sessionsList);
        }

        public IActionResult History()
        {
            ViewBag.ViewName = "Session";
            var sessionList = adminService.GetAllSessions();
            return View(sessionList);
           
        }
        public IActionResult NewSession()
        {
            SessionVM session = new SessionVM();
            session.Sessions = adminService.GetAllSessions();
            session.Stores = adminService.GetAllStores();
            return View(session);
           // var storesList = adminService.GetAllStores();
           // return View(storesList);
        }
        public IActionResult Details(int? id)
        {
          
            return View();
        }

    }
}