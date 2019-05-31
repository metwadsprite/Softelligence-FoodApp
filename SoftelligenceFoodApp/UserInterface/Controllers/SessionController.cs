using BusinessLogic;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using BusinessLogic;
using System.Collections.Generic;
using BusinessLogic.BusinessExceptions;
using System;

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

            try
            {
                session.Session = adminService.GetActiveSession();
                session.HasActiveSession = true;
                session.Stores = session.Session.Stores;
            }
            catch(SessionNotFoundException)
            {
                session.HasActiveSession = false;
                session.Stores = adminService.GetAllStores();
            }
            return View(session);

        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            Session session = adminService.GetSessionById(id.Value);
            return View(session);
        }
        [HttpPost]
        public IActionResult Create([FromForm]Session newSession)
        {
            if (ModelState.IsValid)
            {
                newSession.StartTime = DateTime.Now;
                var store1 = adminService.GetStoreById(1);
                var store2 = adminService.GetStoreById(10);

                newSession.AddStore(store1);
                newSession.AddStore(store2);
                adminService.StartSession(newSession);
            }
            return RedirectToAction("NewSession");
        }
        public IActionResult CloseRestaurant()
        {
            return View();
        }
      
    }
}