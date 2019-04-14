using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class MainDashboardDataObject : ObjectGraphType<MainDashboardData>
    {
        public MainDashboardDataObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "MainDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<MainDeviceObject>(
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
            Field(x => x.Pressure);
            Field(x => x.AbsolutePressure);
            Field<TrendEnumeration>("pressureTrend", resolve: ctx => ctx.Source.PressureTrend);
            Field(x => x.CO2).Name("co2");
            Field(x => x.Humidity);
            Field(x => x.Noise);
        }
    }
}
