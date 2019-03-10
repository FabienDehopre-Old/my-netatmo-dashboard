using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public enum Trend
    {
        [EnumMember(Value = "down")]
        Down,
        [EnumMember(Value = "up")]
        Up,
        [EnumMember(Value = "stable")]
        Stable
    }
}
