using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {

        private IUsersRepository userRepository;
        User user;

        public HomeController(IPersistenceContext dataContext)
        {
            this.userRepository = dataContext.GetUsersRepository();
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var isUser = userRepository.FindUser(userEmail);

            if(isUser == false)
            {
                User user = new User() { Email = userEmail, Name = HttpContext.User.Identity.Name };
                userRepository.Create(user);
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
