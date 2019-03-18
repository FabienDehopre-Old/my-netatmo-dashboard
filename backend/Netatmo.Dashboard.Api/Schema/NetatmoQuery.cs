using GraphQL.Types;
using Netatmo.Dashboard.Api.Helpers;

namespace Netatmo.Dashboard.Api.Schema
{
    public class NetatmoQuery : ObjectGraphType
    {
        public NetatmoQuery(ContextServiceLocator contextServiceLocator)
        {
            Field<ListGraphType<StationType>>(
                "stations",
                resolve: context => contextServiceLocator.StationRepository.GetAll()
            );

            Field<StationType>(
                "station",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => contextServiceLocator.StationRepository.GetOne(context.GetArgument<int>("id"))
            );

            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: context => contextServiceLocator.CountryRepository.GetAll()
            );

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "code" }),
                resolve: context => contextServiceLocator.CountryRepository.GetOne(context.GetArgument<string>("code"))
            );
        }
    }
}
