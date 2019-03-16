using System.Collections.Generic;

namespace Netatmo.Dashboard.Api.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Altitude { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Timezone { get; set; }
        public string StaticMap { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Device> Devices { get; set; }
        public virtual Country Country { get; set; }
    }
}