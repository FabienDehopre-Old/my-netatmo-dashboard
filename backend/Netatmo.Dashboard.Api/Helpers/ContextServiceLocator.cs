using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Api.Repositories;
using System;

namespace Netatmo.Dashboard.Api.Helpers
{
    public class ContextServiceLocator
    {
        public IStationRepository StationRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IStationRepository>();
        public ICountryRepository CountryRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ICountryRepository>();

        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
    }
}
