using System.Collections.Generic;

namespace Netatmo.Dashboard.Api.Models
{
    public abstract class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Firmware { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public ICollection<DashboardData> DashboardData { get; set; }
    }

    public class MainDevice : Device
    {
        public int WifiStatus { get; set; }
    }

    public class ModuleDevice : Device
    {
        public int RfStatus { get; set; }
        public Battery Battery { get; set; }
        public ModuleDeviceType Type { get; set; }
    }
}
