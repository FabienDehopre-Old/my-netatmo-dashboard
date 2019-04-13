using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class IndoorModule : Module
    {
        [DataMember(Name = "dashboard_data")]
        public IndoorModuleDashboardData DashboardData { get; set; }
    }
}
