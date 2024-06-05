using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Utility
{
    public static class StaticDetails
    {
      public  enum ApiType
        {
GET,
POST,
PUT,
DELETE
        }
        public static string Sessiontoken = "JWTtoekn";
        public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Administrator"));
            }

            if (!await roleManager.RoleExistsAsync("Librarian"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Librarian"));
            }
            if (!await roleManager.RoleExistsAsync("Member"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Member"));
            }
            if (!await roleManager.RoleExistsAsync("Guest"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Guest"));
            }
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Student"));
            }

            // Add more roles as needed...
        }
    }
   
}
