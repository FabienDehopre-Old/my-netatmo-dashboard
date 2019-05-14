using Microsoft.AspNetCore.Builder;
using GraphQL.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.ComponentModel.DataAnnotations;

namespace Netatmo.Dashboard.Api.Options
{
    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        [Required]
        public CacheProfileOptions CacheProfiles { get; set; }

        [Required]
        public CompressionOptions Compression { get; set; }

        [Required]
        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

        [Required]
        public GraphQLOptions GraphQL { get; set; }

        [Required]
        public KestrelServerOptions Kestrel { get; set; }
    }
}
