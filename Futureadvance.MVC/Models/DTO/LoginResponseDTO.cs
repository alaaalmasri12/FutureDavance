using Futureadvance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.MVC.Models.DTO
{
    public class LoginResponseDTO
    {
        public Localuser User { get; set; }
        public string Token { get; set; }
    }
}
