using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class RainGaugeModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public RainGaugeModuleDashboardData DashboardData { get; set; }
    }
}
