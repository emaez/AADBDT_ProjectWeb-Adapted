using Microsoft.AspNetCore.Identity;
using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<NRAKOUser> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                SubscriptionModel subscriptionModel = new SubscriptionModel
                {
                    DailyUploadLimit = 1_000,
                    MaxNumberOfPhotos = 10_000,
                    MaxUploadSize = 10_000,
                    Name = "SubscriptionModel",
                    Price = 100
                };

                NRAKOUser user = new NRAKOUser
                {                    
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    Enabled = true,
                    //SubscriptionModelId = 3
                    SubscriptionModel = subscriptionModel
                };

                IdentityResult result = userManager.CreateAsync(user, "Pa$$w0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
