using System;

namespace Netatmo.Dashboard.Core.Models
{
    public class TemperatureData
    {
        public decimal Current { get; set; }
        public (int Value, DateTimeOffset Timestamp) Min { get; set; }
        public (int Value, DateTimeOffset Timestamp) Max { get; set; }
        public Trend Trend { get; set; }
    }
}