using JsonSubTypes;
using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(OutdoorModuleDto), ModuleTypeDto.NAModule1)]
    [JsonSubtypes.KnownSubType(typeof(WindGaugeModuleDto), ModuleTypeDto.NAModule2)]
    [JsonSubtypes.KnownSubType(typeof(RainGaugeModuleDto), ModuleTypeDto.NAModule3)]
    [JsonSubtypes.KnownSubType(typeof(IndoorModuleDto), ModuleTypeDto.NAModule4)]
    public class ModuleDto
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public ModuleTypeDto Type { get; set; }

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