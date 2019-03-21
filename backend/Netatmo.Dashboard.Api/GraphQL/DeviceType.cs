using GraphQL.Types;
using Netatmo.Dashboard.Api.Helpers;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.GraphQL
{
    public class DeviceInterface : InterfaceGraphType<Device>
    {
        public DeviceInterface()
        {
            Name = "Device";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Firmware);
            Field<StationType>("station");
            Field<ListGraphType<DashboardDataInterface>>("dashboardData");
        }
    }

    public class MainDeviceType : ObjectGraphType<MainDevice>
    {
        public MainDeviceType(ContextServiceLocator contextServiceLocator)
        {
            Name = "MainDevice";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Firmware);
            Field<StationType>(
                "station",
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.Source.StationId),
                description: "Station that manage this device."
            );
            Field<ListGraphType<DashboardDataInterface>>(
                "dashboardData",
                resolve: ctx => contextServiceLocator.DashboardDataRepository.GetAll(ctx.Source.Id)
            );
            Field(x => x.WifiStatus);

            Interface<DeviceInterface>();
            IsTypeOf = obj => obj is MainDevice;
        }
    }

    public class ModuleDeviceType : ObjectGraphType<ModuleDevice>
    {
        public ModuleDeviceType(ContextServiceLocator contextServiceLocator)
        {
            Name = "ModuleDevice";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Firmware);
            Field<StationType>(
                "station",
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.Source.StationId),
                description: "Station that manage this device."
            );
            Field<ListGraphType<DashboardDataInterface>>(
                "dashboardData",
                resolve: ctx => contextServiceLocator.DashboardDataRepository.GetAll(ctx.Source.Id)
            );
            Field(x => x.RfStatus);
            Field(x => x.BatteryVp);
            Field(x => x.BatteryPercent);
            Field<ModuleDeviceTypeEnum>("type", resolve: ctx => ctx.Source.Type);

            Interface<DeviceInterface>();
            IsTypeOf = obj => obj is ModuleDevice;
        }
    }
}
