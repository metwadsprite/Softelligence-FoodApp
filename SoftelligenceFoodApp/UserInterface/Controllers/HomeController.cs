using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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

            if (isUser == false)
            {
                var ass = HttpContext
                userEmail = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Emai
                var ceva = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var altceva = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                User user = new User() { Email = userEmail, Name = userName };
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
