using Auth0.ManagementApi.Models;
using Newtonsoft.Json;

namespace Netatmo.Dashboard.Api.DTOs
{
    public class VerifyEmailJobRequestWithClientId : VerifyEmailJobRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
