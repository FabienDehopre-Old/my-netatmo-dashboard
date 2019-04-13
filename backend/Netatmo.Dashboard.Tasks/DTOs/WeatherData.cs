using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public class WeatherData
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "time_exec")]
        public decimal ExecutionDuration { get; set; }

        [DataMember(Name = "time_server")]
        public int ServerTime { get; set; }

        [DataMember(Name = "body")]
        public WeatherDataBody Body { get; set; }
    }
}
