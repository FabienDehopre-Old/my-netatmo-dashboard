namespace Netatmo.Dashboard.Core.Models
{
    public abstract class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Firmware { get; set; }
        public virtual Station Station { get; set; }
    }

    public class MainDevice : Device
    {
        public int WifiStatus { get; set; }
        // TODO: current dashboard data
    }

    public abstract class ModuleDevice : Device
    {
        public int RfStatus { get; set; }
        public Battery Battery { get; set; }
    }

    public class OutdoorModuleDevice : ModuleDevice
    {
        // TODO: current dashboard data
    }

    public class WindGaugeModuleDevice : ModuleDevice
    {
        // TODO: current dashboard data
    }

    public class RainGaugeModuleDevice : ModuleDevice
    {
        // TODO: current dashboard data
    }

    public class IndoorModuleDevice : ModuleDevice
    {
        // TODO: current dashboard data
    }
}
