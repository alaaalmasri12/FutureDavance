using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Core.Models.DTO
{
    public class LoginResponseDTO
    {
        public Localuser User { get; set; }
        public string Token { get; set; }
    }
}
