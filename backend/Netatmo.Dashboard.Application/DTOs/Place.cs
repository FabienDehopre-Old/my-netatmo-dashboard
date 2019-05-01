using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class Place
    {
        [JsonProperty("altitude")]
        public int Altitude { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("location")]
        public decimal[] Location { get; set; }
    }
}