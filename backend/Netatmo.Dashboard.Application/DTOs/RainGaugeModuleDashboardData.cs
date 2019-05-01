using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class RainGaugeModuleDashboardData : ModuleDashboardData
    {
        [JsonProperty("Rain")]
        public decimal Rain { get; set; }

        [JsonProperty("sum_rain_24")]
        public decimal SumRainOver24h { get; set; }

        [JsonProperty("sum_rain_1")]
        public decimal SumRainOver1h { get; set; }
    }
}
