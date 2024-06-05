using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.MVC.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email Address  is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password  is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
