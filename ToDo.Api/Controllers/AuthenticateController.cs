using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _bus;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IMediator bus, IJwtAuthManager jwtAuthManager, ILogger<AuthenticateController> logger)
        {
            _bus = bus;
            _jwtAuthManager = jwtAuthManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _bus.Send(new GetUserCommand(request));

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.Username, claims, DateTime.Now);

            return Ok(new LoginResult
            {
                UserName = request.Username,
                Role = "User",
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }


        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName); // can be more specific to ip, user agent, device name, etc.
            _logger.LogInformation($"User [{userName}] logged out the system.");

            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                    return Unauthorized();

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.RefreshAsync(request.RefreshToken, accessToken, DateTime.Now);

                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");

                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                _logger.LogError(e.Message);
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _bus.Send(new GetUserCommand(user));

                if (existingUser != null)
                {
                    _logger.LogInformation("User already in use");
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>() {
                                "User already in use"
                            },
                        Success = false
                    });                    
                }

                var isCreated = await _bus.Send(new AddUserCommand(user));
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,isCreated.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

                if (isCreated != null)
                {
                    var jwtToken =  _jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);
                    return Ok(jwtToken);
                }
                else
                {
                    _logger.LogInformation("User already in use");
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string> { "N�o foi poss�vel criar o usu�rio" },
                        Success = false
                    }); ;
                }
            }

            _logger.LogError("Invalid payload");
            return BadRequest(new RegistrationResponse()
            {
                
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }
    }
}