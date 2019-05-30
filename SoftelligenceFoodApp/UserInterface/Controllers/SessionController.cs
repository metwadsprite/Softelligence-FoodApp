using BusinessLogic;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using BusinessLogic;
using System.Collections.Generic;
using BusinessLogic.BusinessExceptions;

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
                session.hasActiveSession = true;
            }
            catch(SessionNotFoundException)
            {
                session.hasActiveSession = false;
            }

            session.Stores = session.Session.Stores;
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
                adminService.StartSession(newSession);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}