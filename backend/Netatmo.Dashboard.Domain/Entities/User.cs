using System;
using System.Collections.Generic;

namespace Netatmo.Dashboard.Domain.Entities
{
    public class User
    {
        public User()
        {
            Stations = new HashSet<Station>();
        }

        public int Id { get; set; }
        public string Uid { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string UpdateJobId { get; set; }
        public FeelLikeAlgo? FeelLike { get; set; }
        public PressureUnit? PressureUnit { get; set; }
        public Unit? Unit { get; set; }
        public WindUnit? WindUnit { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
    }
}
