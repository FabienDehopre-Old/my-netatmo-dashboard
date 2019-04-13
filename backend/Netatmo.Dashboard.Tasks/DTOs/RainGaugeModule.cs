using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class RainGaugeModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public RainGaugeModuleDashboardData DashboardData { get; set; }
    }
}
