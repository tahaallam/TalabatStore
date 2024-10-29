using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
            var User = new AppUser()
            {
                DisplayName = "Taha Sayed",
                Email = "tahas224@gamil.com",
                UserName = "tahas224",
                PhoneNumber = "01121188770"
            };
            await userManager.CreateAsync(User ,"P@$$w0rd");
            }
        }
    }
}
