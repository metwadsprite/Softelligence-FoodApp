using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;

namespace UserInterface.Data
{
    public class CustomIdentityDbContext : IdentityDbContext
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options)
            : base(options)
        {
        }
       
    }
}
