using GraphQL.Types;
using Netatmo.Dashboard.Api.Helpers;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.GraphQL
{
    public class DashboardDataUnion : UnionGraphType
    {
        public DashboardDataUnion()
        {
            //Type<MainDashboardDataType>();
            Type<OutdoorDashboardDataType>();
            Type<WindGaugeDashboardDataType>();
            Type<RainGaugeDashboardDataType>();
            Type<IndoorDashboardDataType>();
        }
    }

    public class MainDashboardDataType : ObjectGraphType<MainDashboardData>
    {
        public MainDashboardDataType(ContextServiceLocator contextServiceLocator)
        {
            Name = "MainDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<MainDeviceType>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Temperature);
            Field(x => x.TemperatureMin);
            Field(x => x.TemperatureMinTimestamp);
            Field(x => x.TemperatureMax);
            Field(x => x.TemperatureMaxTimestamp);
            Field<TrendEnum>("temperatureTrend", resolve: ctx => ctx.Source.TemperatureTrend);
            Field(x => x.Pressure);
            Field(x => x.AbsolutePressure);
            Field<TrendEnum>("pressureTrend", resolve: ctx => ctx.Source.PressureTrend);
            Field(x => x.CO2).Name("co2");
            Field(x => x.Humidity);
            Field(x => x.Noise);
        }
    }

    public class OutdoorDashboardDataType : ObjectGraphType<OutdoorDashboardData>
    {
        public OutdoorDashboardDataType(ContextServiceLocator contextServiceLocator)
        {
            Name = "OutdoorDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceType>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Temperature);
            Field(x => x.TemperatureMin);
            Field(x => x.TemperatureMinTimestamp);
            Field(x => x.TemperatureMax);
            Field(x => x.TemperatureMaxTimestamp);
            Field<TrendEnum>("temperatureTrend", resolve: ctx => ctx.Source.TemperatureTrend);
            Field(x => x.Humidity);
        }
    }

    public class WindGaugeDashboardDataType : ObjectGraphType<WindGaugeDashboardData>
    {
        public WindGaugeDashboardDataType(ContextServiceLocator contextServiceLocator)
        {
            Name = "WindGaugeDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceType>(
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

    public class RainGaugeDashboardDataType : ObjectGraphType<RainGaugeDashboardData>
    {
        public RainGaugeDashboardDataType(ContextServiceLocator contextServiceLocator)
        {
            Name = "RainGaugeDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceType>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Rain);
            Field(x => x.Sum1H);
            Field(x => x.Sum24H);
        }
    }

    public class IndoorDashboardDataType : ObjectGraphType<IndoorDashboardData>
    {
        public IndoorDashboardDataType(ContextServiceLocator contextServiceLocator)
        {
            Name = "IndoorDashboardData";

            Field(x => x.Id);
            Field(x => x.TimeUtc);
            Field<ModuleDeviceType>(
                "device",
                resolve: ctx => contextServiceLocator.DeviceRepository.GetOne(ctx.Source.DeviceId),
                description: "Device from which the data come from"
            );
            Field(x => x.Temperature);
            Field(x => x.TemperatureMin);
            Field(x => x.TemperatureMinTimestamp);
            Field(x => x.TemperatureMax);
            Field(x => x.TemperatureMaxTimestamp);
            Field<TrendEnum>("temperatureTrend", resolve: ctx => ctx.Source.TemperatureTrend);
            Field(x => x.CO2).Name("co2");
            Field(x => x.Humidity);
        }
    }
}
