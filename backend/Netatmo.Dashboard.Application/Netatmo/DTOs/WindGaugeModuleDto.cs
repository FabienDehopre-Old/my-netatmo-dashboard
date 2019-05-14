using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class WindGaugeModuleDto : ModuleDto
    {
        [JsonProperty("dashboard_data")]
        public WindGaugeModuleDashboardDataDto DashboardData { get; set; }
    }
}
