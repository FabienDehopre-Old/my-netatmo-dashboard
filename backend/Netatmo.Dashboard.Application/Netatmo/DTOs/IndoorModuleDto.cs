using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class IndoorModuleDto : ModuleDto
    {
        [JsonProperty("dashboard_data")]
        public IndoorModuleDashboardDataDto DashboardData { get; set; }
    }
}
