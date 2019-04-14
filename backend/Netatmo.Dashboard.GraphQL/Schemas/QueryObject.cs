using GraphQL.Types;
using Netatmo.Dashboard.GraphQL.Helpers;
using Netatmo.Dashboard.GraphQL.Types;

namespace Netatmo.Dashboard.GraphQL.Schemas
{
    public class QueryObject : ObjectGraphType
    {
        public QueryObject(ContextServiceLocator contextServiceLocator)
        {
            Field<ListGraphType<StationObject>>(
                "stations",
                resolve: ctx => contextServiceLocator.StationRepository.GetAll()
            );

            Field<StationObject>(
                "station",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: ctx => contextServiceLocator.StationRepository.GetOne(ctx.GetArgument<int>("id"))
            );

            Field<ListGraphType<CountryObject>>(
                "countries",
                resolve: ctx => contextServiceLocator.CountryRepository.GetAll()
            );

            Field<CountryObject>(
                "country",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "code" }),
                resolve: ctx => contextServiceLocator.CountryRepository.GetOne(ctx.GetArgument<string>("code"))
            );
        }
    }
}
