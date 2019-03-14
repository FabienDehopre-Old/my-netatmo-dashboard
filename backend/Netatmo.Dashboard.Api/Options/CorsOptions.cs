namespace Netatmo.Dashboard.Api.Options
{
    public class CorsOptions
    {
        public string[] AllowedOrigins { get; set; }
        public string[] AllowedMethods { get; set; }
        public string[] AllowedHeaders { get; set; }
    }
}
