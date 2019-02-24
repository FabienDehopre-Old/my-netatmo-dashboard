using System;
using System.Collections.Generic;

namespace Netatmo.Dashboard.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public bool Enabled { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public List<Station> Stations { get; set; }
    }
}
