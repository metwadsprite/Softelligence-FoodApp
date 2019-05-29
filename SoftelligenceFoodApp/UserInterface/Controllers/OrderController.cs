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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Index()
        {
            activeSession = sessionRepository.GetActiveSession();

            return View(activeSession);
        }

        [Authorize]
        [HttpGet]
        public IActionResult PlaceRestaurantOrder(int? id)
        {
            activeSession = sessionRepository.GetActiveSession();
            Store storeWithId = activeSession.Stores.SingleOrDefault(store => store.Id == id);

            curOrder = new PlaceRestaurantOrderVM();
            curOrder.OrderStoreId = storeWithId.Id;
            curOrder.StoreName = storeWithId.Name;
            curOrder.Image = storeWithId.Menu.Image;
            curOrder.Hyperlink = storeWithId.Menu.Hyperlink;

            return View(curOrder);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PlaceOrder([FromForm]PlaceRestaurantOrderVM orderVM)
        {
            var curOrder = new PlaceRestaurantOrderVM
            {
                StoreName = orderVM.StoreName,
                OrderStoreId = orderVM.OrderStoreId,
                Option = orderVM.Option,
                Price = orderVM.Price,
                Image = orderVM.Image,
                Hyperlink = orderVM.Hyperlink
            };

            var storeToPlace = new Store
            {
                Name = orderVM.StoreName,
            };

            var menuItem = new MenuItem
            {
                Details = orderVM.Option,
                Price = orderVM.Price
            };
            user.SelectCurrentUser(1);

            user.PlaceOrder(storeToPlace, menuItem);

            return View(curOrder);
        }


        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

    }
}