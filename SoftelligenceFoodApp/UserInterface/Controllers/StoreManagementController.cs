using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class StoreManagementController : Controller
    {
        private IStoresRepository storeRepo;

        public StoreManagementController(IPersistenceContext dataContext)
        {
            storeRepo = dataContext.GetStoresRepository();
        }

        public IActionResult Index()
        {
            var storeList = storeRepo.GetAll();
            return View(storeList);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}