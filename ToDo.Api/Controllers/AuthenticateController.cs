using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDo.Domain.Auth;
using ToDo.Domain.Entities;
using ToDo.Domain.Services.Interfaces;
using AuthResult = ToDo.Domain.Auth.AuthResult;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _bus;

        public AuthenticateController(IUserService userService, IMediator bus)
        {
            _userService = userService;
            _bus = bus;
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
            var refreshToken = RefreshTokenService.CreateRefreshToken(token);
            user.Password = "";

            return new
            {
                user = user,
                token = token,
                refreshToken = refreshToken
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var userExists = await _userService.GetByUserName(user.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new AuthResult { Success = false, Errors = new List<string> { "User already exists!" } });

            user = new User()
            {
                Password = user.Password,
                Username = user.Username
            };
            try
            {
                _userService.Create(user);
                return Ok(new AuthResult { Success = true });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new AuthResult { Success = false, Errors = new List<string> { "User creation failed! Please check user details and try again." } });
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var res = await VerifyToken(tokenRequest);

                if (res == null)                
                    return BadRequest(new AuthResult(){Errors = new List<string>() {"Invalid tokens"},Success = false });                

                return Ok(res);
            }

            return BadRequest(new AuthResult(){ Errors = new List<string>() { "Invalid payload" }, Success = false });
        }

        private async Task<AuthResult> VerifyToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // This validation function will make sure that the token meets the validation parameters
                // and its an actual jwt token not just a random string
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                // Now we need to check if the token has a valid security algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Will get the time stamp in unix time
                var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefaultAsync(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                // we convert the expiry date from seconds to the date
                var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
                        Success = false
                    };
                }

                // Check the token we got if its saved in the db
                //var storedRefreshToken = await _apiDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                var storedRefreshToken = await _bus.Send(tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "refresh token doesnt exist" },
                        Success = false
                    };
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has expired, user needs to relogin" },
                        Success = false
                    };
                }

                // check if the refresh token has been used
                if (storedRefreshToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been used" },
                        Success = false
                    };
                }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been revoked" },
                        Success = false
                    };
                }

                // we are getting here the jwt token id
                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // check the id that the recieved token has against the id saved in the db
                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "the token doenst mateched the saved token" },
                        Success = false
                    };
                }

                storedRefreshToken.IsUsed = true;
                _apiDbContext.RefreshTokens.Update(storedRefreshToken);
                await _apiDbContext.SaveChangesAsync();

                var dbUser = await UserManager.FindByIdAsync(storedRefreshToken.UserId);
                return await CreateToken(dbUser);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}