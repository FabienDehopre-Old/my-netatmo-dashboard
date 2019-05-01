using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class WeatherDataBody
    {
        [JsonProperty("devices")]
        public Device[] Devices { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
