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

        }
      
        public IActionResult Details()
        {
            Session session = new Session();
            return View();
        }
    }
}