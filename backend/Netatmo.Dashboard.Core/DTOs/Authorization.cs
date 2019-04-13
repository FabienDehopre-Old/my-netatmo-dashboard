using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Core.DTOs
{
    [DataContract]
    public class Authorization
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
