using BusinessLogic;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using BusinessLogic.BusinessExceptions;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Business.Exceptions;

namespace UserInterface.Controllers
{
    public class SessionController : Controller
    {
        private readonly AdminService adminService;
        public SessionController(AdminService adminService)
        {
            this.adminService = adminService;

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var sessionsList = adminService.GetAllSessions();
            return View(sessionsList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult History()
        {
            ViewBag.ViewName = "Session";
            var sessionList = adminService.GetAllSessions();
            return View(sessionList);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult NewSession()
        {
            SessionVM session = new SessionVM();

            try
            {
                session.Session = adminService.GetActiveSession();
                session.HasActiveSession = true;
                session.Stores = session.Session.Stores
                                                .ToList();
            }
            catch (SessionNotFoundException)
            {
                session.HasActiveSession = false;
                session.Stores = adminService.GetAllStores()
                                            .ToList();
            }
            return View(session);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            Session session = adminService.GetSessionById(id.Value);
            return View(session);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromForm]SessionVM newSession)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Session sessionToCreate = new Session();
                    sessionToCreate.StartTime = DateTime.Now;

                    for (int i = 0; i < newSession.Stores.Count(); i++)
                    {
                        if (newSession.SelectedStores[i])
                        {
                            var currentStore = adminService.GetStoreById(newSession.Stores[i].Id);
                            currentStore.IsActive = true;
                            adminService.UpdateStore(currentStore);
                            sessionToCreate.AddStore(currentStore);
                        }

                    }
                    adminService.StartSession(sessionToCreate);
                }
                catch(SessionIsEmptyException)
                {
                    return RedirectToAction("NewSession");
                }
            }
            return RedirectToAction("NewSession");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CloseRestaurant(int? id)
        {
            Store store = adminService.GetStoreById(id.Value);
            return View(store);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CloseRestaurant([FromForm]Store storeToRemoveFromSession)
        {
            if (ModelState.IsValid)
            {
                Session currentSession = adminService.GetActiveSession();

                storeToRemoveFromSession = adminService.GetStoreById(storeToRemoveFromSession.Id);                

                adminService.CloseRestaurant(storeToRemoveFromSession, currentSession);
            }
            return RedirectToAction("NewSession");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CloseSession()
        {
            Session session = adminService.GetActiveSession();
            return View(session);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CloseSession([FromForm]Session sessionToRemove)
        {
            if (ModelState.IsValid)
            {
                sessionToRemove = adminService.GetActiveSession();

                adminService.CloseSession(sessionToRemove);
            }
            return RedirectToAction("Index");
        }
    }
}
