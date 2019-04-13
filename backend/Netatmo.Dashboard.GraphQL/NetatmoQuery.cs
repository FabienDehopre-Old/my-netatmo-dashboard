using GraphQL.Types;
using Netatmo.Dashboard.GraphQL.Helpers;

namespace Netatmo.Dashboard.GraphQL
{
    public class NetatmoQuery : ObjectGraphType
    {
        public NetatmoQuery(ContextServiceLocator contextServiceLocator)
        {
            Field<ListGraphType<StationType>>(
                "stations",
                resolve: ctx => contextServiceLocator.StationRepository.GetAll()
            );

            Field<StationType>(
                "station",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.GetArgument<int>("id"))
            );

            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: ctx => contextServiceLocator.CountryRepository.GetAll()
            );

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "code" }),
                resolve: ctx => contextServiceLocator.CountryRepository.GetOne(ctx.GetArgument<string>("code"))
            );
        }
    }
}
