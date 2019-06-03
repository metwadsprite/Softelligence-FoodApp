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
using BusinessLogic.BusinessExceptions;

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

            var sessionHistory = sessionRepository.GetAll();
            var suggestedOrders = new HashSet<Order>();

            foreach (var session in sessionHistory)
            {
                foreach (var order in session.Orders)
                {
                    if (order.Store.Id == storeWithId.Id)
                    {
                        suggestedOrders.Add(order);
                    }
                }
            }

            curOrder = new PlaceRestaurantOrderVM
            {
                OrderStoreId = storeWithId.Id,
                StoreName = storeWithId.Name,
                Image = storeWithId.Menu.Image,
                Hyperlink = storeWithId.Menu.Hyperlink,
                Suggestions = suggestedOrders
            };

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

            PlaceRestaurantOrderVM orderVM = new PlaceRestaurantOrderVM();
            orderVM.Option = userOrder.Details;
            orderVM.Image = userOrder.Store.Menu.Image;
            orderVM.StoreName = userOrder.Store.Name;
            orderVM.Price = userOrder.Price;

            return View(orderVM);

        }

        [Authorize]
        [HttpPost]
        public IActionResult ModifyOrder([FromForm]PlaceRestaurantOrderVM orderVM)
        {
            activeSession = sessionRepository.GetActiveSession();
            var userEmail = HttpContext.User.Identity.Name;

            userService.SelectCurrentUser(userEmail);
            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            userService.LoadOrder(userOrder);

            var storeToPlace = userOrder.Store;

            var menuItem = new MenuItem
            {
                Details = orderVM.Option,
                Price = orderVM.Price
            };

            userService.ChangeOrder(storeToPlace, menuItem, userOrder.Id);

            orderVM.StoreName = storeToPlace.Name;
            return View(orderVM);
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

        public IActionResult GetHistory()
        {
            ICollection<Session> sessionHistory = sessionRepository.GetAll();
            var userEmail = HttpContext.User.Identity.Name;

            userService.SelectCurrentUser(userEmail);
            //var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            ICollection<Order> orderHistoy = new List<Order>();

            foreach(var session in sessionHistory)
            {
                if(session.IsActive == false)
                {
                    foreach(var order in session.Orders)
                    {
                        if (order.IsActive == false)
                        {
                            orderHistoy.Add(order);
                        }
                    }
                }
            }

            return View(orderHistoy);
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}