using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class UserDto
    {
        [JsonProperty("mail")]
        public string Email { get; set; }

        [JsonProperty("administrative")]
        public UserAdminDataDto AdminData { get; set; }
    }
}
