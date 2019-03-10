using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class WindGaugeModuleDashboardData : ModuleDashboardData
    {
        [DataMember(Name = "WindStrength")]
        public decimal WindStrength { get; set; }

        [DataMember(Name = "WindAngle")]
        public int WindAngle { get; set; }

        [DataMember(Name = "GustStrength")]
        public decimal GustStrength { get; set; }

        [DataMember(Name = "GustAngle")]
        public int GustAngle { get; set; }
    }
}
