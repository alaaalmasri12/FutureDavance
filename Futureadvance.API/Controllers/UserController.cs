using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Core.Models;
using Futureadvance.Core.Models.DTO;
using Futureadvance.Repostiory.Data;
using Futureadvance.Repostiory.GenericRepoistory;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Futureadvance.API.Controllers
{
    [Route("api/userAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IunitofWork _unitofwork;
        protected Apiresponse response;
        private FutureadvanceDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IunitofWork unitofwork, FutureadvanceDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._unitofwork = unitofwork;
            this.response = new();
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {       
                var loginresponse = await _unitofwork.userRepository.LoginAsync(loginRequestDTO);
                if (loginresponse == null || string.IsNullOrWhiteSpace(loginresponse.Token))
                {
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.IsSucssess = false;

                    response.ErrorMessages.Add("username or password is invalid");
                    return BadRequest(response);
                }
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.IsSucssess = true;
                response.Result = loginresponse;
                return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisteritionRequestDto registeritionRequestDto)
        {
            if (await UserExist(registeritionRequestDto.Email))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.IsSucssess = false;
                response.ErrorMessages.Add("username already exist");
                return BadRequest(response);
            }

            var Registerresponse = await _unitofwork.userRepository.Regiserasync(registeritionRequestDto);
            if (Registerresponse==null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.IsSucssess = false;
                response.ErrorMessages.Add("Error while register");
                return BadRequest(response);
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.IsSucssess = true;
            response.Result = Registerresponse;
            return Ok(response);
        }
        [HttpGet("GetAllusers")]
        public async Task<IActionResult> GetAllusers(int UserID)
        {

            var Registerresponse = await _unitofwork.userRepository.getAllusers();
            var RegisterresponseResult = Registerresponse.Where(x=>x.Id != UserID);
            return Json(new { data = RegisterresponseResult });

        }
        [HttpPost("updateuser")]
        public async Task<IActionResult> Updateuser(UpdateuserDto registeritionRequestDto)
        {
          
            var Registerresponse =  await _unitofwork.userRepository.Updateuser(registeritionRequestDto);
            if (Registerresponse == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.IsSucssess = false;
                response.ErrorMessages.Add("Error while updating user");
                return BadRequest(response);
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.IsSucssess = true;
            response.Result = Registerresponse;
            return Ok(response);
        }
        [HttpPost("Deleteuser")]
        public async Task<IActionResult> Deleteuser(int userId)
        {

            var Registerresponse = await _unitofwork.userRepository.DeleteUserAsync(userId);
            if (Registerresponse == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.IsSucssess = false;
                response.ErrorMessages.Add("Error while deleting user");
                return BadRequest(response);
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.IsSucssess = true;
            response.Result = Registerresponse;
            return Ok(response);
        }
        private async Task<bool> UserExist(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName.ToString().ToLower() == username.ToString().ToLower());
        }
    }
}
