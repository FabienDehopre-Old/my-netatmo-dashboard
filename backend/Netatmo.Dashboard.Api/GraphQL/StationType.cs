using GraphQL.Types;
using Netatmo.Dashboard.Api.Helpers;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.GraphQL
{
    public class StationType : ObjectGraphType<Station>
    {
        public StationType(ContextServiceLocator contextServiceLocator)
        {
            Name = "Station";
            // Description = "";

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Altitude);
            Field(x => x.City);
            Field<CountryType>(
                "country",
                resolve: context => contextServiceLocator.CountryRepository.GetOne(context.Source.CountryCode),
                description: "Country in which the wheather station is located."
            );
            Field(x => x.Latitude);
            Field(x => x.Longitude);
            Field(x => x.Timezone);
            Field(x => x.StaticMap);
            Field<ListGraphType<DeviceInterface>>(
                "devices",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: context => contextServiceLocator.DeviceRepository.GetAll(context.Source.Id)
            );
        }
    }
}
