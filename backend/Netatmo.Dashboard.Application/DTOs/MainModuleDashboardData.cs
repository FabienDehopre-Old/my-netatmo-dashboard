using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class MainModuleDashboardData : ModuleDashboardData
    {
        [JsonProperty("Temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty("CO2")]
        public int CO2 { get; set; }

        [JsonProperty("Humidity")]
        public int Humidity { get; set; }

        [JsonProperty("Noise")]
        public int Noise { get; set; }

        [JsonProperty("Pressure")]
        public decimal Pressure { get; set; }

        [JsonProperty("AbsolutePressure")]
        public decimal AbsolutePressure { get; set; }

        [JsonProperty("min_temp")]
        public decimal MinTemperature { get; set; }

        [JsonProperty("max_temp")]
        public decimal MaxTemperature { get; set; }

        [JsonProperty("date_min_temp")]
        public int MinTemperatureTimeUtc { get; set; }

        [JsonProperty("date_max_temp")]
        public int MaxTemperatureTimeUtc { get; set; }

        [JsonProperty("temp_trend")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Trend TemperatureTrend { get; set; }

        [JsonProperty("pressure_trend")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Trend PressureTrend { get; set; }
    }
}