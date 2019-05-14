using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class AuthorizeUrl
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }
    }
}
