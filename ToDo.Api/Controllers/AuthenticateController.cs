using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Auth;
using ToDo.Domain.Entities;
using ToDo.Domain.Services.Interfaces;
using Response = ToDo.Domain.Auth.Response;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticateController( IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = await _userService.GetByUserNameAndPassword(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }
        
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] User user)  
        {  
            var userExists = await _userService.GetByUserName(user.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ToDo.Domain.Auth.Response { Status = "Error", Message = "User already exists!" });  
  
            user = new User()  
            {  
               Password = user.Password,
               Username = user.Username
            };
            try
            {
                _userService.Create(user);  
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
            }
        }
    }
}