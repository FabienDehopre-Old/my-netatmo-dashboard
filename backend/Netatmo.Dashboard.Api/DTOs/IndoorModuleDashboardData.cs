using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class IndoorModuleDashboardData : ModuleDashboardData
    {
        [DataMember(Name = "Temperature")]
        public decimal Temperature { get; set; }

        [DataMember(Name = "CO2")]
        public int CO2 { get; set; }

        [DataMember(Name = "Humidity")]
        public int Humidity { get; set; }

        [DataMember(Name = "min_temp")]
        public decimal MinTemperature { get; set; }

        [DataMember(Name = "max_temp")]
        public decimal MaxTemperature { get; set; }

        [DataMember(Name = "date_min_temp")]
        public int MinTemperatureTimeUtc { get; set; }

        [DataMember(Name = "date_max_temp")]
        public int MaxTemperatureTimeUtc { get; set; }

        [DataMember(Name = "temp_trend")]
        public Trend TemperatureTrend { get; set; }
    }
}
