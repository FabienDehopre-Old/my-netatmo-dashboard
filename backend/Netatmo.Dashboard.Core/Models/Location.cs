using System;

namespace Netatmo.Dashboard.Core.Models
{
    public class Location
    {
        public double Altitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public (decimal Latitude, decimal Longitude) GeoLocation { get; set; }
        public string Timezone { get; set; }
        public Uri StaticMap { get; set; }
    }
}
