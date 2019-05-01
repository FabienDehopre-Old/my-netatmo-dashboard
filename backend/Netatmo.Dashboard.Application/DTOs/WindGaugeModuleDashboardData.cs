using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class WindGaugeModuleDashboardData : ModuleDashboardData
    {
        [JsonProperty("WindStrength")]
        public decimal WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        public int WindAngle { get; set; }

        [JsonProperty("GustStrength")]
        public decimal GustStrength { get; set; }

        [JsonProperty("GustAngle")]
        public int GustAngle { get; set; }
    }
}
