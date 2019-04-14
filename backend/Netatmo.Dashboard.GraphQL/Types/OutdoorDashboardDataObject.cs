using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class OutdoorDashboardDataObject : ObjectGraphType<OutdoorDashboardData>
    {
        public OutdoorDashboardDataObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "OutdoorDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceObject>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Temperature);
            Field(x => x.TemperatureMin);
            Field(x => x.TemperatureMinTimestamp);
            Field(x => x.TemperatureMax);
            Field(x => x.TemperatureMaxTimestamp);
            Field<TrendEnumeration>("temperatureTrend", resolve: ctx => ctx.Source.TemperatureTrend);
            Field(x => x.Humidity);
        }
    }
}
