using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class OutdoorModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public OutdoorModuleDashboardData DashboardData { get; set; }
    }
}
