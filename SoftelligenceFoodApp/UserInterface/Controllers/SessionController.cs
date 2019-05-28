using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using BusinessLogic;
using System.Collections.Generic;

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
            Session sessionToStart = new Session();
            session.Sessions = adminService.GetAllSessions();
            session.Stores = adminService.GetAllStores();
            sessionToStart.Stores = (ICollection<Store>)session.Stores;
            adminService.StartSession(sessionToStart);
            return View(session);

        }
        public IActionResult Details(int? id)
        {
            return View();
        }
    }
}