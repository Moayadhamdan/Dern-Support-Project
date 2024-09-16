using  DernSupport_BackEnd.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace DernSupport_BackEnd.Repositories.Interfaces
{
    public interface IUser
    {
        // Register
        public Task<UserDto> Register(RegisterdDto registerdDto, ModelStateDictionary modelState);


        // Login 
        public Task<UserDto> Login(string username, string password);


        // Get Token 
        public Task<UserDto> GetToken(ClaimsPrincipal claimsPrincipal);
    }
}
