using System.Collections.Generic;

namespace Netatmo.Dashboard.Api.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Flag { get; set; }
        public string NameEN { get; set; }
        public string NameBR { get; set; }
        public string NamePT { get; set; }
        public string NameNL { get; set; }
        public string NameHR { get; set; }
        public string NameFA { get; set; }
        public string NameDE { get; set; }
        public string NameES { get; set; }
        public string NameFR { get; set; }
        public string NameJA { get; set; }
        public string NameIT { get; set; }
        public virtual List<Station> Stations { get; set; }
    }
}
