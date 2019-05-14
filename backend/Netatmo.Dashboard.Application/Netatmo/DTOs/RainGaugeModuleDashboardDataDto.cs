using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class RainGaugeModuleDashboardDataDto : ModuleDashboardDataDto
    {
        [JsonProperty("Rain")]
        public decimal Rain { get; set; }

        [JsonProperty("sum_rain_24")]
        public decimal SumRainOver24h { get; set; }

        [JsonProperty("sum_rain_1")]
        public decimal SumRainOver1h { get; set; }
    }
}
