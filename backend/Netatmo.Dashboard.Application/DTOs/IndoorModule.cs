using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class IndoorModule : Module
    {
        [JsonProperty("dashboard_data")]
        public IndoorModuleDashboardData DashboardData { get; set; }
    }
}
