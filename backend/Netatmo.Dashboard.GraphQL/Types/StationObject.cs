using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class StationObject : ObjectGraphType<Station>
    {
        public StationObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "Station";
            // Description = "";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Altitude);
            Field(x => x.City);
            //Field<CountryObject>(
            //    "country",
            //    resolve: ctx => contextServiceLocator.CountryRepository.GetOne(ctx.Source.CountryCode),
            //    description: "Country in which the wheather station is located."
            //);
            Field(x => x.Latitude);
            Field(x => x.Longitude);
            Field(x => x.Timezone);
            Field(x => x.StaticMap);
            Field<ListGraphType<DeviceUnion>>(
                "devices",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: ctx => contextServiceLocator.DeviceRepository.GetAll(ctx.Source.Id)
            );
        }
    }
}
