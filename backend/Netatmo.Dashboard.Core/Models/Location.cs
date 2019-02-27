using System;

namespace Netatmo.Dashboard.Core.Models
{
    public class Location
    {
        public decimal Altitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public GeoPoint GeoLocation { get; set; }
        public string Timezone { get; set; }
        public string StaticMap { get; set; }
    }
}
