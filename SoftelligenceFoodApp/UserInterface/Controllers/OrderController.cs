using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Abstractions;
using BusinessLogic;
using Logic.Implementations;
using UserInterface.Models;
using EF.DataAccess;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private ISessionsRepository sessionRepository;
        Session activeSession;
        UserService user;

        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.user = new UserService(dataContext);
        }

        public IActionResult Index()
        {
            activeSession = sessionRepository.GetActiveSession();

            return View(activeSession);
        }

        [HttpGet]
        public IActionResult PlaceRestaurantOrder(int id, [FromForm]string option, [FromForm]decimal price)
        {
            activeSession = sessionRepository.GetActiveSession();
            Store storeWithId = activeSession.Stores.SingleOrDefault(store => store.Id == id);

            PlaceRestaurantOrderVM restaurant = new PlaceRestaurantOrderVM();
            restaurant.Option = option;
            restaurant.Price = price;
            restaurant.OrderStore = storeWithId;

            return View(restaurant);
        }

        [HttpPost]
        public IActionResult PlaceOrder(PlaceRestaurantOrderVM restaurant)
        {
            return View(restaurant);
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

    }
}