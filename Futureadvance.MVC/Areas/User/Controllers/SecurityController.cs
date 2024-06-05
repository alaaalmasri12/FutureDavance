using Futureadvance.API.Customizeresponses;
using Futureadvance.Core.Models;
using Futureadvance.MVC.Models.DTO;
using Futureadvance.MVC.Services.Iservices;
using Futureadvance.Repostiory.Migrations;
using Futureadvance.Repostiory.Models;
using Futureadvance.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Futureadvance.MVC.Areas.User.Controllers
{
    [Area("User")]
    public class SecurityController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private Apiresponse _apiresponse;
        private readonly IUserservice _userservice;
        public SecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole<int>> roleManager,IUserservice userservice)
        {
            this._userManager = userManager;
            this.signInManager = signInManager;
            this._httpContextAccessor = httpContextAccessor;
            this._roleManager = roleManager;
            this._userservice = userservice;
            this._apiresponse = new Apiresponse();

        }
        [HttpGet]
        public async Task<IActionResult> LoginForm()
        {
           
              
                return View();

            
        }
        [HttpPost]
        public async Task<IActionResult> LoginForm(LoginRequestDTO userobj)
        {
            if (ModelState.IsValid)
            {

                Apiresponse response = await _userservice.loginAsync<Apiresponse>(userobj);
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
                    var result = await signInManager.PasswordSignInAsync(userobj.Email, userobj.password, false, lockoutOnFailure: false);
                    TempData["Sucssess"] = $"Welcome {userobj.Email}";

                    LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                    HttpContext.Session.SetString(StaticDetails.Sessiontoken, model.Token);
                    HttpContext.Session.SetString("userEmail", userobj.Email);
                    if (model.User.Role == "Admin")
                    {
                        return RedirectToAction("Index", "AdminDashbored", new { area = "Admin" });

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });

                    }
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

        [HttpGet]
        public async Task<IActionResult> RegisterForm()
        {
            return View();
        }
        public async Task<IActionResult> logout()
        {
          await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDetails.Sessiontoken, "");
            HttpContext.Session.SetString(".AspNetCore.Session", "");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Session");

            TempData["Sucssess"] = $"You Logout Sucssessfully";

            return RedirectToAction(nameof(LoginForm));
        }
        [HttpPost]
        public async Task<IActionResult> RegisterForm(RegisteritionRequestDto registeritionRequestDto)
        {
            if (ModelState.IsValid)
            {
                Apiresponse response = await _userservice.RegisterAsync<Apiresponse>(registeritionRequestDto);
                if (response.IsSucssess && response != null)
                {
                    TempData["Sucssess"] = $"Register is Sucssessfull";

                    return RedirectToAction(nameof(LoginForm));
                }
                else if (!response.IsSucssess)
                {
                    TempData["error"] = response.ErrorMessages.FirstOrDefault().ToString();
                    return View();

                }
                else
                {
                    return View();

                }
            }
            else
            {
                return View(registeritionRequestDto);

            }
        }
    }
}
