using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class WindGaugeModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public WindGaugeModuleDashboardData DashboardData { get; set; }
    }
}
