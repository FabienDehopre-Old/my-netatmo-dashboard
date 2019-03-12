using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    [KnownType(typeof(MainModuleDashboardData))]
    [KnownType(typeof(OutdoorModuleDashboardData))]
    [KnownType(typeof(WindGaugeModuleDashboardData))]
    [KnownType(typeof(RainGaugeModuleDashboardData))]
    [KnownType(typeof(IndoorModuleDashboardData))]
    public abstract class ModuleDashboardData
    {
        [DataMember(Name = "time_utc")]
        public long TimeUtc { get; set; }
    }
}
