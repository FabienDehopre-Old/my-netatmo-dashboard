using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class AuthorizationDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
