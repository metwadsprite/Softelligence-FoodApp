using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Abstractions;
using BusinessLogic;
using Logic.Implementations;
using UserInterface.Models;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.BusinessExceptions;

namespace UserInterface.Controllers
{
    public class OrderController : Controller
    {
        private readonly ISessionsRepository sessionRepository;
        private Session activeSession;
        private PlaceRestaurantOrderVM curOrder;
        private readonly UserService userService;

        public OrderController(IPersistenceContext dataContext)
        {
            this.sessionRepository = dataContext.GetSessionsRepository();
            this.userService = new UserService(dataContext);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            activeSession = null;
            var userEmail = HttpContext.User.Identity.Name;
            OrderActiveVM orderActiveVM = new OrderActiveVM() { Session = null, OrderIsActive = true };
            try
            {
                activeSession = sessionRepository.GetActiveSession();
                orderActiveVM.Session = activeSession;
            }
            catch(SessionNotFoundException)
            {
                return View(orderActiveVM);
            }
     
            userService.SelectCurrentUser(userEmail);

            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            if (userOrder == null)
            {
                return View(orderActiveVM);

            }
            else
            {
                orderActiveVM.Order = new Order();
                orderActiveVM.Order.Store = new Store();
                orderActiveVM.Order.Store.Name = userOrder.Store.Name;
                orderActiveVM.Order.Price = userOrder.Price;
                orderActiveVM.Order.User = new User();
                orderActiveVM.Order.User.Name = userOrder.User.Name;
                orderActiveVM.Order.Details = userOrder.Details;
                orderActiveVM.OrderIsActive = userOrder.IsActive;  
                if (userOrder.IsActive == false)
                {
                    return View(orderActiveVM);
                }

                return RedirectToAction("ModifyOrderDisplay", "Order");
            }
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public IActionResult PlaceRestaurantOrder(int? id)
        {
            activeSession = sessionRepository.GetActiveSession();
            Store storeWithId = activeSession.Stores.SingleOrDefault(store => store.Id == id);

            var suggestions = storeWithId.GetProductSuggestions();

            curOrder = new PlaceRestaurantOrderVM
            {
                OrderStoreId = storeWithId.Id,
                StoreName = storeWithId.Name,
                Image = storeWithId.Menu.Image,
                Hyperlink = storeWithId.Menu.Hyperlink,
                Suggestions = suggestions.Select(menuItem => new OrderVM { Option = menuItem.Details, Price = menuItem.Price })
                                         .ToList<OrderVM>()
            };

            return View(curOrder);
        }

        [Authorize(Roles = "User, Admin")]
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

        [Authorize(Roles = "User, Admin")]
        public IActionResult ModifyOrderDisplay()
        {
            activeSession = sessionRepository.GetActiveSession();

            var userEmail = HttpContext.User.Identity.Name;
            userService.SelectCurrentUser(userEmail);

            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            if(userOrder.IsActive == false)
            {
                return RedirectToAction("Index", "Order");
            }

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

        [Authorize(Roles = "User, Admin")]
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

        [Authorize(Roles = "User, Admin")]
        public IActionResult DeleteOrder()
        {
            activeSession = sessionRepository.GetActiveSession();
            var userEmail = HttpContext.User.Identity.Name;

            userService.SelectCurrentUser(userEmail);
            var userOrder = activeSession.Orders.FirstOrDefault(order => order.User.Email == userEmail);

            userService.CancelOrder(userOrder);

            return View();
        }

        [Authorize(Roles = "User, Admin")]
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
                        if (order.IsActive == false && order.User.Email == userEmail)
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