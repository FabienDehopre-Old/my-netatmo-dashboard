namespace Netatmo.Dashboard.Core.Models
{
    public abstract class DashboardData
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
    }

    public class MainDashboardData : DashboardData
    {
        public TemperatureData Temperature { get; set; }
        public (decimal Value, decimal Absolute, Trend Trend) Pressure { get; set; }
        public int CO2 { get; set; }
        public int Humidity { get; set; }
        public int Noise { get; set; }
        public virtual MainDevice Device { get; set; }
    }

    public class OutdoorDashboardData : DashboardData
    {
        public TemperatureData Temperature { get; set; }
        public int Humidity { get; set; }
        public virtual OutdoorModuleDevice Device { get; set; }
    }

    public class WindGaugeDashboardData : DashboardData
    {
        public int WindStrength { get; set; }
        public int WindAngle { get; set; }
        public int GustStrength { get; set; }
        public int GustAngle { get; set; }
        public virtual WindGaugeModuleDevice Device { get; set; }
    }

    public class RainGaugeDashboardData : DashboardData
    {
        public decimal Rain { get; set; }
        public decimal Sum1H { get; set; }
        public decimal Sum24H { get; set; }
        public virtual RainGaugeModuleDevice Device { get; set; }
    }

    public class IndoorDashboardData : DashboardData
    {
        public TemperatureData Temperature { get; set; }
        public int CO2 { get; set; }
        public int Humidity { get; set; }
        public virtual IndoorModuleDevice Device { get; set; }
    }
}
