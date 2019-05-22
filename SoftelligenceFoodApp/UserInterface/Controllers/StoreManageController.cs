using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class StoreManageController : Controller
    {
        private IStoresRepository storesRepository;
        public StoreManageController(IPersistenceContext dataContext)
        {
            storesRepository = dataContext.GetStoresRepository();

        }
        public IActionResult Index()
        {
            ViewBag.ViewName = "StoreManagement";
            var storesList = storesRepository.GetAll();
            return View(storesRepository);
        }
    }
}