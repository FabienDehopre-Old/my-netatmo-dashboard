﻿using System.Collections.Generic;

namespace Netatmo.Dashboard.Domain.Entities
{
    public class Country
    {
        public Country()
        {
            Stations = new HashSet<Station>();
        }

        public string Code { get; set; }
        public string Flag { get; set; }
        public string NameBR { get; set; }
        public string NameDE { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
        public string NameFA { get; set; }
        public string NameFR { get; set; }
        public string NameHR { get; set; }
        public string NameIT { get; set; }
        public string NameJA { get; set; }
        public string NameNL { get; set; }
        public string NamePT { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
    }
}
