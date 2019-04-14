using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{

    public class MainDeviceObject : ObjectGraphType<MainDevice>
    {
        public MainDeviceObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "MainDevice";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Firmware);
            Field<StationObject>(
                "station",
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.Source.StationId),
                description: "Station that manage this device."
            );
            Field<ListGraphType<MainDashboardDataObject>>(
                "dashboardData",
                resolve: ctx => contextServiceLocator.DashboardDataRepository.GetAll(ctx.Source.Id)
            );
            Field(x => x.WifiStatus);
        }
    }
}
