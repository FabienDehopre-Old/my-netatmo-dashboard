using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public abstract class ModuleDashboardData
    {
        [DataMember(Name = "time_utc")]
        public long TimeUtc { get; set; }
    }
}
