using System.Collections.Generic;

namespace Netatmo.Dashboard.Core.Models
{
    public abstract class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Firmware { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
    }

    public class MainDevice : Device
    {
        public int WifiStatus { get; set; }
        public List<MainDashboardData> DashboardData { get; set; }
    }

    public abstract class ModuleDevice : Device
    {
        public int RfStatus { get; set; }
        public Battery Battery { get; set; }
    }

    public class OutdoorModuleDevice : ModuleDevice
    {
        public List<OutdoorDashboardData> DashboardData { get; set; }
    }

    public class WindGaugeModuleDevice : ModuleDevice
    {
        public List<WindGaugeDashboardData> DashboardData { get; set; }
    }

    public class RainGaugeModuleDevice : ModuleDevice
    {
        public List<RainGaugeDashboardData> DashboardData { get; set; }
    }

    public class IndoorModuleDevice : ModuleDevice
    {
        public List<IndoorDashboardData> DashboardData { get; set; }
    }
}
