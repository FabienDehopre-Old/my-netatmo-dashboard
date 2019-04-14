using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class RainGaugeDashboardDataObject : ObjectGraphType<RainGaugeDashboardData>
    {
        public RainGaugeDashboardDataObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "RainGaugeDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceObject>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Rain);
            Field(x => x.Sum1H);
            Field(x => x.Sum24H);
        }
    }
}
