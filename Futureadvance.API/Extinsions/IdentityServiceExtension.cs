using Futureadvance.Repostiory.Data;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Identity;

namespace Futureadvance.API.Extinsions
{
    static class IdentityServiceExtension
    {
        public static IServiceCollection addIdentityservices(this IServiceCollection Services, IConfiguration config)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Configure password requirements
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                options.User.RequireUniqueEmail = false;
                // Configure lockout settings
                options.Lockout.MaxFailedAccessAttempts = 5;

                // Configure user settings
                options.User.RequireUniqueEmail = true;

                // Other configuration options...
            }).AddEntityFrameworkStores<FutureadvanceDbContext>().AddDefaultTokenProviders();

            return Services;
        }
    }
}
