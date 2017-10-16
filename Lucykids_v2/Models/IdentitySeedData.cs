using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lucykids_v2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public static class IdentitySeedData
    {
        const string adminUser = "slovakivo@hotmail.com";
        const string adminPassword = "miK19$roskop";
        const string roleName = "Admin";



    public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user==null)
            {
                user = new IdentityUser("slovakivo@hotmail.com");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
