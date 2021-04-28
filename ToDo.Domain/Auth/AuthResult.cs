using System.Collections.Generic;

namespace ToDo.Domain.Auth
{
    public class AuthResult
    {
        //public string Status { get; set; }  
        //public string Message { get; set; }  
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}