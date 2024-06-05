using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Repostiory.Data
{
    public class FutureadvanceDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public FutureadvanceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
