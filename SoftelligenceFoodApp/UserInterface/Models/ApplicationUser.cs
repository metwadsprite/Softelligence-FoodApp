using Microsoft.AspNetCore.Identity;

namespace UserInterface.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

    }
}
