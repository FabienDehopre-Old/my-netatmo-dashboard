using GraphQL.Types;
using Netatmo.Dashboard.Api.Helpers;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.Schema
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Code);
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
            Field<ListGraphType<StationType>>(
                "stations",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => contextServiceLocator.StationRepository.GetAll(context.Source.Code)
            );
        }
    }
}
