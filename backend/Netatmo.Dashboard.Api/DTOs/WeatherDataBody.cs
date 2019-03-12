using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class WeatherDataBody
    {
        [DataMember(Name = "devices")]
        public IEnumerable<Device> Devices { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
