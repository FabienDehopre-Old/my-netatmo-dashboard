using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class Device
    {
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public ModuleType Type { get; set; }

        [DataMember(Name = "module_name")]
        public string ModuleName { get; set; }

        [DataMember(Name = "firmware")]
        public int FirmwareVersion { get; set; }

        [DataMember(Name = "wifi_status")]
        public int WifiStatus { get; set; }

        [DataMember(Name = "station_name")]
        public string StationName { get; set; }

        [DataMember(Name = "data_type")]
        public string[] DataType { get; set; }

        [DataMember(Name = "place")]
        public Place Place { get; set; }

        [DataMember(Name = "dashboard_data")]
        public MainModuleDashboardData DashboardData { get; set; }

        [DataMember(Name = "modules")]
        public Module[] Modules { get; set; }
    }
}
