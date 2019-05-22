using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class StoreManageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ViewName = "StoreManagement";
            return View();
        }
    }
}