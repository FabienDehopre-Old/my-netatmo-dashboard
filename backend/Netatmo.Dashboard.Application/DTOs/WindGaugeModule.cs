using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class WindGaugeModule : Module
    {
        [JsonProperty("dashboard_data")]
        public WindGaugeModuleDashboardData DashboardData { get; set; }
    }
}
