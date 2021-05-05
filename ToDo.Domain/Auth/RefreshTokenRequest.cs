using System.Text.Json.Serialization;

namespace ToDo.Domain.Auth
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
