using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Web_Api_Net5.Utils
{
    public static class DataSeed
    {
        //private readonly UserManager<User> _userManager;

        //public DataSeed(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        public static void SeedData(IServiceProvider serviceProvider)
        {
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            if ( _userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@mail.com"
                };
                var result = _userManager.CreateAsync(user, "Adm1nP@ss").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            
        }
    }
}
