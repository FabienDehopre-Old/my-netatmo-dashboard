using JsonSubTypes;
using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(OutdoorModule), ModuleType.NAModule1)]
    [JsonSubtypes.KnownSubType(typeof(WindGaugeModule), ModuleType.NAModule2)]
    [JsonSubtypes.KnownSubType(typeof(RainGaugeModule), ModuleType.NAModule3)]
    [JsonSubtypes.KnownSubType(typeof(IndoorModule), ModuleType.NAModule4)]
    public class Module
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public ModuleType Type { get; set; }

        [JsonProperty("module_name")]
        public string ModuleName { get; set; }

        [JsonProperty("data_type")]
        public string[] DataType { get; set; }

        [JsonProperty("firmware")]
        public int FirmwareVersion { get; set; }

        [JsonProperty("rf_status")]
        public int RFStatus { get; set; }

        [JsonProperty("battery_vp")]
        public int BatteryPower { get; set; }

        [JsonProperty("battery_percent")]
        public int BatteryPercentage { get; set; }
    }
}