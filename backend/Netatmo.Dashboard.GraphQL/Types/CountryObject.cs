using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class CountryObject : ObjectGraphType<Country>
    {
        public CountryObject(ContextServiceLocator contextServiceLocator)
        {
            Name = "Country";
            // Description = "";

            Field(x => x.Code).Type(new IdGraphType());
            Field(x => x.Flag);
            Field(x => x.NameEN);
            Field(x => x.NameBR);
            Field(x => x.NamePT);
            Field(x => x.NameNL);
            Field(x => x.NameHR);
            Field(x => x.NameFA);
            Field(x => x.NameDE);
            Field(x => x.NameES);
            Field(x => x.NameFR);
            Field(x => x.NameJA);
            Field(x => x.NameIT);
            Field<ListGraphType<StationObject>>(
                "stations",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: ctx => contextServiceLocator.StationRepository.GetAll(ctx.Source.Code)
            );
        }
    }
}
