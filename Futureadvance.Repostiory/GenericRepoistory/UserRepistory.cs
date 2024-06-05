using AutoMapper;
using Futureadvance.Core.IGenericRepsitory;
using Futureadvance.Core.Models;
using Futureadvance.Core.Models.DTO;
using Futureadvance.Repostiory.Migrations;
using Futureadvance.Repostiory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Futureadvance.Repostiory.GenericRepoistory
{
    public class UserRepistory : IUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenservice _Tokenservice;

      

        public UserRepistory(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<int>> roleManager, IMapper mapper, ITokenservice tokenservice)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._Tokenservice = tokenservice;
        }

        public async Task<Localuser> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userResult = _mapper.Map<Localuser>(user);
            if (user == null)
            {
                var errorMessage = string.Join(", ", "user is not found");
                throw new Exception(errorMessage);
            }

           await _userManager.DeleteAsync(user);
            return userResult;
        }

        public async Task<IReadOnlyList<Localuser>> getAllusers()
        {

            var users =  _userManager.Users.ToList();
            var Result= _mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<Localuser>>(users);
            return Result;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);
                var userResult = _mapper.Map<Localuser>(user);
                if (user == null)
                {
                    return new LoginResponseDTO()
                    {
                        User = null,
                        Token = ""
                    };

                }

                // Sign in the user
                var result = await _signInManager.PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                    {
                        User = userResult,
                        Token = _Tokenservice.createToken(userResult),
                    };

                    return loginResponseDTO;

                }
                else
                {
                    return new LoginResponseDTO()
                    {
                        User = null,
                        Token = ""
                    };
                }

            }
            catch (Exception ex )
            {

                throw ex;
            }
         
        }

        public async Task<Localuser> Regiserasync(RegisteritionRequestDto registeritionRequestDto)
        {
            try
            {
                if(string.IsNullOrEmpty(registeritionRequestDto.Role))
                {
                    registeritionRequestDto.Role = "NotSet";
                }
                var user = new ApplicationUser()
                {
                    UserName = registeritionRequestDto.Email,
                    Email = registeritionRequestDto.Email,
                    Role = registeritionRequestDto.Role,
                };
                var userResult = _mapper.Map<ApplicationUser, Localuser>(user);
                var Result = await _userManager.CreateAsync(user, registeritionRequestDto.Password);
                if (Result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(user.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>(user.Role));
                    }
                    await _userManager.AddToRoleAsync(user, user.Role);
                }
                else
                {
                    var errorMessage = string.Join(", ", Result.Errors.Select(e => e.Description));
                    throw new Exception(errorMessage);
                }
               return userResult;
            }
            catch (Exception ex )
            {

                throw ex;
            }  
        }

        public async Task<Localuser> Updateuser(UpdateuserDto enitiy)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(enitiy.Id.ToString());
                var userResult = _mapper.Map<ApplicationUser, Localuser>(user);
                if (user != null)
                {
                    // Update user properties
                    user.UserName = enitiy.Email;
                    user.Email = enitiy.Email;
                    user.Role = enitiy.Role;
                    var userRoles = await _userManager.GetRolesAsync(user);
                   await _userManager.RemoveFromRolesAsync(user, userRoles);

                    if (!string.IsNullOrEmpty(enitiy.Role))
                    {
                        await _userManager.AddToRoleAsync(user, enitiy.Role);
                    }
                    if (string.IsNullOrEmpty(enitiy.Password) && string.IsNullOrEmpty(enitiy.ConfirmPassword))
                    {
                    await _userManager.UpdateAsync(user);

                    }
                    else
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        // Reset the user's password using the token and the new password
                      await _userManager.ResetPasswordAsync(user, token, enitiy.Password);
                        // Update user in the database
                      await _userManager.UpdateAsync(user);
                        return userResult;
                    }
                }
                else
                {
                    var errorMessage = string.Join(", ", "error updateing user");
                    throw new Exception(errorMessage);
                }
                return userResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }    
            }
        }
 
}