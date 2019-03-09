using System;

namespace Netatmo.Dashboard.Api.Models
{
    public class TemperatureMinMax
    {
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
