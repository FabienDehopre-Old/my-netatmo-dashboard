using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class WeatherDataBodyDto
    {
        [JsonProperty("devices")]
        public DeviceDto[] Devices { get; set; }

        [JsonProperty("user")]
        public UserDto User { get; set; }
    }
}
