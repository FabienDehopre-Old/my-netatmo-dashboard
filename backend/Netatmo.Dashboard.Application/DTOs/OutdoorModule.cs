using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class OutdoorModule : Module
    {
        [JsonProperty("dashboard_data")]
        public OutdoorModuleDashboardData DashboardData { get; set; }
    }
}
