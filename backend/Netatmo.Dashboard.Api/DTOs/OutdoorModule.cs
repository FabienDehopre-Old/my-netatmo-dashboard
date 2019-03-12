using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class OutdoorModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public OutdoorModuleDashboardData DashboardData { get; set; }
    }
}
