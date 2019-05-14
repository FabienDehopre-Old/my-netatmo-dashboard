using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class WeatherDataDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_exec")]
        public decimal ExecutionDuration { get; set; }

        [JsonProperty("time_server")]
        public int ServerTime { get; set; }

        [JsonProperty("body")]
        public WeatherDataBodyDto Body { get; set; }
    }
}
