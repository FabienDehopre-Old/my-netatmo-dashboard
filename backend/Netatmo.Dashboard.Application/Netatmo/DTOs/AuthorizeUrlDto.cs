using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class AuthorizeUrlDto
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
