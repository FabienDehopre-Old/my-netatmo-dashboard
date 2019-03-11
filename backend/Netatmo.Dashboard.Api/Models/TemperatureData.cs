using System;

namespace Netatmo.Dashboard.Api.Models
{
    public class TemperatureData
    {
        public decimal Current { get; set; }
        public virtual TemperatureMinMax Min { get; set; }
        public virtual TemperatureMinMax Max { get; set; }
        public Trend Trend { get; set; }
    }
}