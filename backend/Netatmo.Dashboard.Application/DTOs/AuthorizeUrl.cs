using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class AuthorizeUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
