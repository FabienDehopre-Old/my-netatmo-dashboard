using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class User
    {
        [JsonProperty("mail")]
        public string Email { get; set; }

        [JsonProperty("administrative")]
        public UserAdminData AdminData { get; set; }
    }
}
