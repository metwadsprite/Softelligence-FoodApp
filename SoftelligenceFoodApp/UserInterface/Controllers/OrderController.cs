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
        PlaceRestaurantOrderVM curOrder;
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
        public IActionResult PlaceRestaurantOrder(int? id)
        {
            activeSession = sessionRepository.GetActiveSession();
            Store storeWithId = activeSession.Stores.SingleOrDefault(store => store.Id == id);

            var curOrder = new PlaceRestaurantOrderVM()
            {
                OrderStore = storeWithId,
                Option = "",
                Price = 0.0m
            };

            return View(curOrder);
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromForm]PlaceRestaurantOrderVM orderVM)
        {
            return View(orderVM);
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

    }
}