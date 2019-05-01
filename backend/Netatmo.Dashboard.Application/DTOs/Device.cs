using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class Device
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleType Type { get; set; }

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
        public Place Place { get; set; }

        [JsonProperty("dashboard_data")]
        public MainModuleDashboardData DashboardData { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }
    }
}