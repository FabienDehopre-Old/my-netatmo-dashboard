using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    [KnownType(typeof(OutdoorModule))]
    [KnownType(typeof(WindGaugeModule))]
    [KnownType(typeof(RainGaugeModule))]
    [KnownType(typeof(IndoorModule))]
    public abstract class Module
    {
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public ModuleType Type { get; set; }

        [DataMember(Name = "module_name")]
        public string ModuleName { get; set; }

        [DataMember(Name = "data_type")]
        public string[] DataType { get; set; }

        [DataMember(Name = "firmware")]
        public int FirmwareVersion { get; set; }

        [DataMember(Name = "rf_status")]
        public int RFStatus { get; set; }

        [DataMember(Name = "battery_vp")]
        public int BatteryPower { get; set; }

        [DataMember(Name = "battery_percent")]
        public int BatteryPercentage { get; set; }
    }
}
