using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class RainGaugeModule : Module
    {
        [JsonProperty("dashboard_data")]
        public RainGaugeModuleDashboardData DashboardData { get; set; }
    }
}
