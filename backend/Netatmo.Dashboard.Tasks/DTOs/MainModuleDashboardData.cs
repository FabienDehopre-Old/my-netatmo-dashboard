using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class MainModuleDashboardData : ModuleDashboardData
    {
        [DataMember(Name = "Temperature")]
        public decimal Temperature { get; set; }

        [DataMember(Name = "CO2")]
        public int CO2 { get; set; }

        [DataMember(Name = "Humidity")]
        public int Humidity { get; set; }

        [DataMember(Name = "Noise")]
        public int Noise { get; set; }

        [DataMember(Name = "Pressure")]
        public decimal Pressure { get; set; }

        [DataMember(Name = "AbsolutePressure")]
        public decimal AbsolutePressure { get; set; }

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

        [DataMember(Name = "pressure_trend")]
        public Trend PressureTrend { get; set; }
    }
}
