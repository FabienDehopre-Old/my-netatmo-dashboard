using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class RainGaugeModuleDto : ModuleDto
    {
        [JsonProperty("dashboard_data")]
        public RainGaugeModuleDashboardDataDto DashboardData { get; set; }
    }
}
