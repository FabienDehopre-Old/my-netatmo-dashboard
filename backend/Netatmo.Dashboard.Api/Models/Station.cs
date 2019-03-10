using System.Collections.Generic;

namespace Netatmo.Dashboard.Api.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Device> Devices { get; set; }
    }
}