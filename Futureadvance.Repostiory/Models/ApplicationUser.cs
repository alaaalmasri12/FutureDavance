using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Repostiory.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Role { get; set; }
        
    }
}
