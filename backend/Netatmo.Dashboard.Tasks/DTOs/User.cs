using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Tasks.DTOs
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
