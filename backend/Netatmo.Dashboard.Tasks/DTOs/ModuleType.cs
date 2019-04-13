using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
{
    [DataContract]
    public enum ModuleType
    {
        [EnumMember(Value = "NAMain")]
        Main,
        [EnumMember(Value = "NAModule1")]
        Outdoor,
        [EnumMember(Value = "NAModule2")]
        WindGauge,
        [EnumMember(Value = "NAModule3")]
        RainGauge,
        [EnumMember(Value = "NAModule4")]
        Indoor,
    }
}
