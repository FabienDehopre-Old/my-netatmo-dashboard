using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "mail")]
        public string Email { get; set; }

        [DataMember(Name = "administrative")]
        public UserAdminData AdminData { get; set; }
    }
}
