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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private ISessionsRepository sessionRepository;
        Session activeSession;
        PlaceRestaurantOrderVM curOrder;
        private readonly UserService userService;

        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.userService = new UserService(dataContext);
        }

        [Authorize]
        public IActionResult Index()
        {
            activeSession = null;
            var userEmail = HttpContext.User.Identity.Name;
            try
            {
                activeSession = sessionRepository.GetActiveSession();
            }
            catch(SessionNotFoundException)
            {
                return View(activeSession);
            }
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
        }

        [Authorize]
        [HttpGet]
        public IActionResult PlaceRestaurantOrder(int? id)
        {
            activeSession = sessionRepository.GetActiveSession();
            Store storeWithId = activeSession.Stores.SingleOrDefault(store => store.Id == id);

            var sessionHistory = sessionRepository.GetAll();
            var suggestedOrders = new HashSet<OrderVM>();

            foreach (var session in sessionHistory)
            {
                foreach (var order in session.Orders)
                {
                    if (order.Store.Id != id)
                    {
                        continue;
                    }
                    var orderToSuggest = new OrderVM
                    {
                        Option = order.Details,
                        Price = order.Price
                    };

                    suggestedOrders.Add(orderToSuggest);
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
                Order = new OrderVM
                {
                    Option = orderVM.Order.Option,
                    Price = orderVM.Order.Price
                },
                Image = orderVM.Image,
                Hyperlink = orderVM.Hyperlink,
                UserEmail = userService.user.Email
            };

            var storeToPlace = sessionRepository.GetActiveSession().Stores.FirstOrDefault(store => store.Name == orderVM.StoreName);

            var menuItem = new MenuItem
            {
                Details = orderVM.Order.Option,
                Price = orderVM.Order.Price
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

            PlaceRestaurantOrderVM orderVM = new PlaceRestaurantOrderVM()
            {
                Order = new OrderVM
                {
                    Option = userOrder.Details,
                    Price = userOrder.Price
                },
                StoreName = userOrder.Store.Name,
                Image = userOrder.Store.Menu.Image
            };

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
                Details = orderVM.Order.Option,
                Price = orderVM.Order.Price
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