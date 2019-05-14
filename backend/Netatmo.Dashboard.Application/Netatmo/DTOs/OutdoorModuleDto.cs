using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class OutdoorModuleDto : ModuleDto
    {
        [JsonProperty("dashboard_data")]
        public OutdoorModuleDashboardDataDto DashboardData { get; set; }
    }
}
