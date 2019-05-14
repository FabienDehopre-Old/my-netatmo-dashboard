using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Netatmo.Dashboard.Application.Netatmo.DTOs
{
    public class DeviceDto
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeDto Type { get; set; }

        [JsonProperty("module_name")]
        public string ModuleName { get; set; }

        [JsonProperty("firmware")]
        public int FirmwareVersion { get; set; }

        [JsonProperty("wifi_status")]
        public int WifiStatus { get; set; }

        [JsonProperty("station_name")]
        public string StationName { get; set; }

        [JsonProperty("data_type")]
        public string[] DataType { get; set; }

        [JsonProperty("place")]
        public PlaceDto Place { get; set; }

        [JsonProperty("dashboard_data")]
        public MainModuleDashboardDataDto DashboardData { get; set; }

        [JsonProperty("modules")]
        public ModuleDto[] Modules { get; set; }
    }
}