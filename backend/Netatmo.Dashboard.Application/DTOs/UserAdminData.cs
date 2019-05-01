using Newtonsoft.Json;

namespace Netatmo.Dashboard.Application.DTOs
{
    public class UserAdminData
    {
        [JsonProperty("reg_locale")]
        public string RegionalLocale { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("unit")]
        public int Unit { get; set; }

        [JsonProperty("windunit")]
        public int WindUnit { get; set; }

        [JsonProperty("pressureunit")]
        public int PressureUnit { get; set; }

        [JsonProperty("feel_like_algo")]
        public int FeelLikeAlgo { get; set; }
    }
}