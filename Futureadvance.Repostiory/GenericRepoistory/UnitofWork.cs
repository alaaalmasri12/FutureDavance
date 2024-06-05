using AutoMapper;
using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Repostiory.Data;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Repostiory.GenericRepoistory
{
    public class UnitofWork : IunitofWork
    {
        private readonly FutureadvanceDbContext _DbContext;
        public IUserRepository userRepository { get; set; }

        public UnitofWork(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<int>> roleManager, IMapper mapper, ITokenservice tokenservice)
        {
            userRepository = new UserRepistory(userManager,signInManager,roleManager, mapper,tokenservice);
        }

       

        public async Task<int> SaveAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
