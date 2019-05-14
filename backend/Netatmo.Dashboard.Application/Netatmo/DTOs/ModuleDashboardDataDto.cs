using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class ModuleDashboardDataDto
    {
        [JsonProperty("time_utc")]
        public long TimeUtc { get; set; }
    }
}
