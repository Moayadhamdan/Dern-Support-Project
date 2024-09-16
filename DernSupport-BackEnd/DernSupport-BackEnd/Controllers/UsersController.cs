using DernSupport_BackEnd.Models.DTO;
using DernSupport_BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DernSupport_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUser _userService;

        public UsersController(IUser context)
        {
            _userService = context;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterdDto registerdDto)
        {
            var user = await _userService.Register(registerdDto, this.ModelState);


            if (ModelState.IsValid)
            {
                return user;
            }


            if (user == null)
            {
                return Unauthorized();
            }

            return BadRequest();
        }


        // login 
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        }


        [Authorize(Roles = "User")]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDto>> Profile()
        {
            return await _userService.GetToken(User);
        }
    }
}
