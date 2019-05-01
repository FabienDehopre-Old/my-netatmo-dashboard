using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class ModuleDashboardData
    {
        [JsonProperty("time_utc")]
        public long TimeUtc { get; set; }
    }
}
