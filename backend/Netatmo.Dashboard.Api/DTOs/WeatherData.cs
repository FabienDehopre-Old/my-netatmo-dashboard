using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
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

    [DataContract]
    public class WeatherDataBody
    {
        [DataMember(Name = "devices")]
        public IEnumerable<Device> Devices { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember(Name = "mail")]
        public string Email { get; set; }

        [DataMember(Name = "administrative")]
        public UserAdminData UserAdminData { get; set; }
    }

    [DataContract]
    public class UserAdminData
    {
        [DataMember(Name = "reg_locale")]
        public string RegionalLocale { get; set; }

        [DataMember(Name = "lang")]
        public string Language { get; set; }

        [DataMember(Name = "unit")]
        public int Unit { get; set; }

        [DataMember(Name = "windunit")]
        public int WindUnit { get; set; }

        [DataMember(Name = "pressureunit")]
        public int PressureUnit { get; set; }

        [DataMember(Name = "feel_like_alog")]
        public int FeelLikeAlgo { get; set; }
    }

    [DataContract]
    public class Device { }
}
