using DernSupport_BackEnd.Models;
using DernSupport_BackEnd.Models.DTO;
using DernSupport_BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Security.Claims;

namespace DernSupport_BackEnd.Repositories.Services
{
    public class IdentitiUserService : IUser
    {

        private UserManager<ApplicationUser> _userManager;
        private JwtTokenService jwtTokenService;
        
        public IdentitiUserService(UserManager<ApplicationUser> Manager, JwtTokenService jwtTokenService)
        {
            _userManager = Manager;
            this.jwtTokenService = jwtTokenService;
        }

        public async Task<UserDto> Register(RegisterdDto registerdDto, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerdDto.UserName,
                Email = registerdDto.Email,
                AccountType = registerdDto.AccountType
            };

            var result = await _userManager.CreateAsync(user, registerdDto.Password);

            if (result.Succeeded)
            {
                
                await _userManager.AddToRolesAsync(user, registerdDto.Roles);


                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    AccountType = user.AccountType
                };
            }

            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? nameof(registerdDto) :
                                error.Code.Contains("Email") ? nameof(registerdDto) :
                                error.Code.Contains("Username") ? nameof(registerdDto) : "";

                modelState.AddModelError(errorCode, error.Description);
            }
            return null;
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            bool passValidation = await _userManager.CheckPasswordAsync(user, password);

            if (passValidation)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenService.GenerateToken(user, System.TimeSpan.FromMinutes(60))
                };
            }
            return null;
        }

        public async Task<UserDto> GetToken(ClaimsPrincipal claimsPrincipal)
        {
        var user = await _userManager.GetUserAsync(claimsPrincipal);

            return new UserDto()
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await jwtTokenService.GenerateToken(user, System.TimeSpan.FromMinutes(60)) 
            };
        }
    }
}