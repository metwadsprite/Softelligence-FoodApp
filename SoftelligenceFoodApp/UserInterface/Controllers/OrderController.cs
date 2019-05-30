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
using System.Security.Claims;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private ISessionsRepository sessionRepository;
        Session activeSession;
        PlaceRestaurantOrderVM curOrder;
        UserService userService;

        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.userService = new UserService(dataContext);
        }

        [Authorize]
        public IActionResult Index()
        {
            activeSession = sessionRepository.GetActiveSession();

            var userEmail = HttpContext.User.Identity.Name;
            userService.SelectCurrentUser(userEmail);

            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            if (userOrder == null)
            {
                return View(activeSession);

            }
            else
            {
                return RedirectToAction("ModifyOrderDisplay", "Order");
            }

            //return RedirectToAction("Back");
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
            var userName = HttpContext.User.Identity.Name;
            userService.SelectCurrentUser(userName);

            var curOrder = new PlaceRestaurantOrderVM
            {
                StoreName = orderVM.StoreName,
                OrderStoreId = orderVM.OrderStoreId,
                Option = orderVM.Option,
                Price = orderVM.Price,
                Image = orderVM.Image,
                Hyperlink = orderVM.Hyperlink,
                UserEmail = userService.user.Email
            };

            var storeToPlace = sessionRepository.GetActiveSession().Stores.FirstOrDefault(store => store.Name == orderVM.StoreName);

            var menuItem = new MenuItem
            {
                Details = orderVM.Option,
                Price = orderVM.Price
            };

            userService.PlaceOrder(storeToPlace, menuItem, userService.user.Email);

            return View(curOrder);
        }

        public IActionResult ModifyOrderDisplay()
        {
            activeSession = sessionRepository.GetActiveSession();
            var userEmail = HttpContext.User.Identity.Name;
            userService.SelectCurrentUser(userEmail);

            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            return View(userOrder);

        }

        [Authorize]
        [HttpPost]
        public IActionResult ModifyOrder([FromForm]Order orderForm)
        {
            activeSession = sessionRepository.GetActiveSession();
            var userEmail = HttpContext.User.Identity.Name;

            userService.SelectCurrentUser(userEmail);
            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            userService.LoadOrder(userOrder);

            var storeToPlace = userOrder.Store;

            var menuItem = new MenuItem
            {
                Details = orderForm.Details,
                Price = orderForm.Price
            };

            userService.ChangeOrder(storeToPlace, menuItem, userOrder.Id);

            return View(orderForm);
        }

        public IActionResult DeleteOrder()
        {
            activeSession = sessionRepository.GetActiveSession();
            var userEmail = HttpContext.User.Identity.Name;

            userService.SelectCurrentUser(userEmail);
            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            userService.CancelOrder(userOrder);

            return View();
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}