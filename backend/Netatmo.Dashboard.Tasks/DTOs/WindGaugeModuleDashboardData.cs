using Netatmo.Dashboard.Tasks.DTOs;
using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
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
