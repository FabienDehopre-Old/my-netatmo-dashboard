using System.Collections.Generic;

namespace Netatmo.Dashboard.Core.Models
{
    public class Station
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public virtual User User { get; set; }
        public List<Device> Devices { get; set; }
    }
}