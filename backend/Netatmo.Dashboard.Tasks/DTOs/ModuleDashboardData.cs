using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public abstract class ModuleDashboardData
    {
        [DataMember(Name = "time_utc")]
        public long TimeUtc { get; set; }
    }
}
