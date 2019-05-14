using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class IndoorModuleDashboardDataDto : ModuleDashboardDataDto
    {
        [JsonProperty("Temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty("CO2")]
        public int CO2 { get; set; }

        [JsonProperty("Humidity")]
        public int Humidity { get; set; }

        [JsonProperty("min_temp")]
        public decimal MinTemperature { get; set; }

        [JsonProperty("max_temp")]
        public decimal MaxTemperature { get; set; }

        [JsonProperty("date_min_temp")]
        public int MinTemperatureTimeUtc { get; set; }

        [JsonProperty("date_max_temp")]
        public int MaxTemperatureTimeUtc { get; set; }

        [JsonProperty("temp_trend")]
        public TrendDto TemperatureTrend { get; set; }
    }
}
