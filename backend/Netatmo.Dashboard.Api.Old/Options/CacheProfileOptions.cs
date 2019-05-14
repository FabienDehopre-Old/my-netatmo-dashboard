using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Netatmo.Dashboard.Api.Options
{
    /// <summary>
    /// The caching options for the application.
    /// </summary>
    public class CacheProfileOptions : Dictionary<string, CacheProfile>
    {
    }
}
