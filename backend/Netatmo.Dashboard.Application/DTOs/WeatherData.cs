using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class WeatherData
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_exec")]
        public decimal ExecutionDuration { get; set; }

        [JsonProperty("time_server")]
        public int ServerTime { get; set; }

        [JsonProperty("body")]
        public WeatherDataBody Body { get; set; }
    }
}
