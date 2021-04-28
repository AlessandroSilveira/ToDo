using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Auth;

namespace ToDo.Domain.Entities
{
    public class User : Entity
    {        
        public string Username { get; set; }
        public string Password { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}