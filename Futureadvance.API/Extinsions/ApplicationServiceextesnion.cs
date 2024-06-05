using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Repostiory.Data;
using Futureadvance.Repostiory.GenericRepoistory;
using Futureadvance.Repostiory.MappingProfileFolder;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Futureadvance.API.Extinsions
{
    static class ApplicationServiceextesnion
    {
        public static IServiceCollection addApplicationservices(this IServiceCollection Services, IConfiguration config)
        {
            Services.AddDbContext<FutureadvanceDbContext>(options =>
          options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            Services.AddScoped(typeof(IUserRepository), typeof(UserRepistory));
           Services.AddAutoMapper(m => m.AddProfile(typeof(UserMappingProfile)));
            Services.AddScoped<ITokenservice, Tokenservice>();
            Services.AddScoped(typeof(IunitofWork), typeof(UnitofWork));

            return Services;
        }
    }
}
