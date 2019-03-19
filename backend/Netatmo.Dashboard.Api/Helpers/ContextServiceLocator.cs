using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Api.Repositories;
using System;

namespace Netatmo.Dashboard.Api.Helpers
{
    public class ContextServiceLocator
    {
        public IStationRepository StationRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IStationRepository>();
        public IDeviceRepository DeviceRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IDeviceRepository>();
        public IDashboardDataRepository DashboardDataRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IDashboardDataRepository>();
        public ICountryRepository CountryRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ICountryRepository>();

        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
    }
}
