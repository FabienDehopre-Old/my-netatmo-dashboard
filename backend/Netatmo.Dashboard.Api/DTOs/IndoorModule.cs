using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class IndoorModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public IndoorModuleDashboardData DashboardData { get; set; }
    }
}
