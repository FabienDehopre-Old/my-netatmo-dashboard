using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class RainGaugeModuleDashboardData : ModuleDashboardData
    {
        [DataMember(Name = "Rain")]
        public decimal Rain { get; set; }

        [DataMember(Name = "sum_rain_24")]
        public decimal SumRainOver24h { get; set; }

        [DataMember(Name = "sum_rain_1")]
        public decimal SumRainOver1h { get; set; }
    }
}
