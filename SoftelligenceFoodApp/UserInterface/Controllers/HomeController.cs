using System.Diagnostics;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;


namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUsersRepository userRepository;
        private readonly UserManager<ApplicationUser> usersManager;


        public HomeController(IPersistenceContext dataContext, UserManager<ApplicationUser> usersManager)
        {
            this.userRepository = dataContext.GetUsersRepository();
            this.usersManager = usersManager;
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var isUser = userRepository.FindUser(userEmail);

            if (isUser == false)
            {


                var identityUser = await usersManager.FindByEmailAsync(userEmail);
                    
                User user = new User() { Email = identityUser.Email, Name = identityUser.Name };
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
