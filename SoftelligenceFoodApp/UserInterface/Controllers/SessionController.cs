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
    public class SessionController : Controller
    {
        private ISessionsRepository sessionRepository;
        AdminService admin;

        public SessionController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.admin = new AdminService(dataContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
        public IActionResult NewSession()
        {
            Session session = new Session();
            admin.StartSession(session);
            return View();
        }

    }
}