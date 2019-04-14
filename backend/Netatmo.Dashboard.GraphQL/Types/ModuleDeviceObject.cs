using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class ModuleDeviceObject : ObjectGraphType<ModuleDevice>
    {
        public ModuleDeviceObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "ModuleDevice";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Firmware);
            Field<StationObject>(
                "station",
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.Source.StationId),
                description: "Station that manage this device."
            );
            Field<ListGraphType<DashboardDataUnion>>(
                "dashboardData",
                resolve: ctx => contextServiceLocator.DashboardDataRepository.GetAll(ctx.Source.Id)
            );
            Field(x => x.RfStatus);
            Field(x => x.BatteryVp);
            Field(x => x.BatteryPercent);
            Field<ModuleDeviceTypeEnumeration>("type", resolve: ctx => ctx.Source.Type);
        }
    }
}
