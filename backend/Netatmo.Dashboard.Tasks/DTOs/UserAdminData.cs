using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
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
}
