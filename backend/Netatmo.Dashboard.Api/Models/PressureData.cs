namespace Netatmo.Dashboard.Api.Models
{
    public class PressureData
    {
        public decimal Value { get; set; }
        public decimal Absolute { get; set; }
        public Trend Trend { get; set; }
    }
}
