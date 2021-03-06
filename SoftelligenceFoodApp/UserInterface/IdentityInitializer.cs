﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using UserInterface.Data;
using UserInterface.Models;

namespace UserInterface
{
    public class IdentityInitializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void EnsureIdentityDb(string connectionString)
        {
            DbContextOptionsBuilder<CustomIdentityDbContext> optionsBuilder = new DbContextOptionsBuilder<CustomIdentityDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var dbContext = new CustomIdentityDbContext(optionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
            }
        }

        public void InitializeDefaultRoles()
        {
            try
            {                

                var role = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();
                if (role == null)
                {
                    roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                }

                role = roleManager.FindByNameAsync("User").GetAwaiter().GetResult();
                if (role == null)
                {
                    roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not initialize " + e.Message);
            }
        }

        public void InitializeDefaultUsers()
        {
            var adminUser = new ApplicationUser() { Name = "Administrator", Email = "admin@admin.com", UserName = "admin@admin.com"};
            var foundUser = userManager.FindByEmailAsync(adminUser.Email).GetAwaiter().GetResult();
            if (foundUser == null)
            { 
                userManager.CreateAsync(adminUser, "P@ssw0rd").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            }

        }
    }
}
