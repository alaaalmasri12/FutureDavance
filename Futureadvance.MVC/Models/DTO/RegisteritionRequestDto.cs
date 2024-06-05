using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.MVC.Models.DTO
{
    public class RegisteritionRequestDto
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email Address  is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password  is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password  is Required")]

        [Compare("Password", ErrorMessage = "The Password and Confirm Password fields do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        [ValidateNever]
        public string Role { get; set; }
    }
}
