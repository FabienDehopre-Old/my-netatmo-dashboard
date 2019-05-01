using System.Collections.Generic;

namespace Netatmo.Dashboard.Domain.Entities
{
    public class Station
    {
        public Station()
        {
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Altitude { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Timezone { get; set; }
        public string StaticMap { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual Country Country { get; set; }
    }
}
