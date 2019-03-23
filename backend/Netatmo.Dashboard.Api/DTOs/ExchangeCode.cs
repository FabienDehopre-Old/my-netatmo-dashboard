using System.Runtime.Serialization;

namespace Netatmo.Dashboard.Api.DTOs
{
    [DataContract]
    public class ExchangeCode
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "returnUrl")]
        public string ReturnUrl { get; set; }
    }
}
