using System.Collections.Generic;

namespace Netatmo.Dashboard.Domain.Entities
{
    public abstract class Device
    {
        protected Device()
        {
            DashboardData = new HashSet<DashboardData>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Firmware { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public virtual ICollection<DashboardData> DashboardData { get; set; }
    }

    public class MainDevice : Device
    {
        public int WifiStatus { get; set; }
    }

    public class ModuleDevice : Device
    {
        public int RfStatus { get; set; }
        public int BatteryVp { get; set; }
        public int BatteryPercent { get; set; }
        public ModuleDeviceType Type { get; set; }
    }
}
