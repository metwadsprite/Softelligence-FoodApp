using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Abstractions;
using BusinessLogic;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private ISessionsRepository sessionRepository;
        Session activeSession;
        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
        }

        public IActionResult Index()
        {
            activeSession = sessionRepository.GetActiveSession();
            return View(activeSession);
        }
    }
}