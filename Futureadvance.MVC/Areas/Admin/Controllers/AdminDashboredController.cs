using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Core.Models;
using Futureadvance.MVC.Models.DTO;
using Futureadvance.MVC.Services.Iservices;
using Futureadvance.Repostiory.Migrations;
using Futureadvance.Repostiory.Models;
using Futureadvance.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Futureadvance.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminDashboredController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public List<IdentityRole> Roles { get; set; }
        private readonly IUserservice _userservice;

        public AdminDashboredController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IUserservice userservice)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userservice = userservice;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                // Use currentUser as needed
            }
            return View();
        }
        public JsonResult getuser()
        {
            var currentUser =  _userManager.GetUserAsync(User);
            return Json(currentUser);

        }
        public async Task <JsonResult> GetCurrentUser()
        {
            var userEmail = HttpContext.Session.GetString("userEmail");


            var currentUser =await _userManager.FindByEmailAsync(userEmail);
            return Json(currentUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentuser = await _userManager.FindByIdAsync(id.ToString());
            var Roleslist = await _roleManager.Roles.ToListAsync();
            var rolesuser = await _userManager.GetRolesAsync(currentuser);
            ViewBag.Roles = Roleslist;
            ViewBag.rolesuser = rolesuser;
            var userdeatils = new RegisteritionRequestDto()
            {
                Id= currentuser.Id,
                Role = currentuser.Role,
                Email = currentuser.Email
            };

            return View(userdeatils);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var currentuser = await _userManager.FindByIdAsync(id.ToString());
            var Roleslist = await _roleManager.Roles.ToListAsync();
            var rolesuser = await _userManager.GetRolesAsync(currentuser);
            ViewBag.Roles = Roleslist;
            ViewBag.rolesuser = rolesuser;
            var userdeatils = new RegisteritionRequestDto()
            {
                Id = currentuser.Id,
                Role = currentuser.Role,
                Email = currentuser.Email
            };

            return View(userdeatils);
        }

        [HttpPost]
        public async Task<IActionResult> Deleteuser(RegisteritionRequestDto userobj)
        {
            Apiresponse response = await _userservice.DeleteuserAsync<Apiresponse>(userobj.Id);
            if(response.IsSucssess)
            {
                TempData["Sucssess"] = $"user has been deleted";
                return RedirectToAction("Index", "AdminDashbored", new { area = "Admin" });
            }
            else
            {
                return View(userobj);
            }
        }
        [HttpPost]
        public async Task<IActionResult> updateuser(RegisteritionRequestDto userobj)
        {
            if (string.IsNullOrEmpty(userobj.ConfirmPassword))

            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
            }
            if (ModelState.IsValid)
            {

                Apiresponse response = await _userservice.UpdateuserAsync<Apiresponse>(userobj);
                if (response == null)
                {
                    TempData["error"] = "user is not found";

                    return View();
                }
                else if (!response.IsSucssess)
                {
                    TempData["error"] = response.ErrorMessages.FirstOrDefault().ToString();
                    return View();

                }
                if (response.IsSucssess && response != null)
                {
                    TempData["Sucssess"] = $"user has been updated";
                    return RedirectToAction("Index", "AdminDashbored", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("customerror", response.ErrorMessages.FirstOrDefault());
                    return View(userobj);
                }

            }
            else
            {
                return View(userobj);
            }

        }
    }
}
