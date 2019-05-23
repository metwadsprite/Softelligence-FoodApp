using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Abstractions;
using BusinessLogic;
using Logic.Implementations;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private ISessionsRepository sessionRepository;
        Session activeSession;
        UserService user;
        Store store;
        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.user = new UserService(dataContext);
        }

        public IActionResult Index()
        {
            try
            {
                activeSession = sessionRepository.GetActiveSession();
            }
            catch(Exception)
            {
            }

            return View(activeSession);
        }

        [HttpGet]
        public IActionResult PlaceRestaurantOrder(int id)
        {
            activeSession.Stores.ElementAt(id);
            return View();
        }

        public IActionResult PlaceOrder()
        {

            return View();
        }

    }
}