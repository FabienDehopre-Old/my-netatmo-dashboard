using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class WindGaugeDashboardDataObject : ObjectGraphType<WindGaugeDashboardData>
    {
        public WindGaugeDashboardDataObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "WindGaugeDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceObject>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.WindStrength);
            Field(x => x.WindAngle);
            Field(x => x.GustStrength);
            Field(x => x.GustAngle);
        }
    }
}
