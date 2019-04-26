using Boxed.AspNetCore;
using GraphQL.Builders;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;
using Netatmo.Dashboard.GraphQL.Types;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.GraphQL.Schemas
{
    public class QueryObject : ObjectGraphType
    {
        public QueryObject(
            IStationRepository stationRepository,
            ICountryRepository countryRepository)
        {
            Name = "Query";
            Description = "The query type represents all the entry points into the netatmo weather data object graph.";

            //FieldAsync<StationObject, Station>(
            //    "station",
            //    "Get a weather station by its unique identifier.",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IdGraphType>>
            //        {
            //            Name = "id",
            //            Description = "The unique identifier of the station."
            //        }),
            //    resolve: context => stationRepository.GetOne(
            //        context.GetArgument<int>("id"),
            //        context.CancellationToken));
            FieldAsync<CountryObject, Country>(
                "country",
                "Get a country by its ISO 3166-1 alpha-2 code.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "code",
                        Description = "The ISO 3166-1 alpha-2 code representing the country.",
                        DefaultValue = "be"
                    }),
                resolve: context => countryRepository.GetOne(
                    context.GetArgument("code", defaultValue: "be"),
                    context.CancellationToken));
            Connection<CountryObject>()
                .Name("countries")
                .Description("List of the countries")
                .Bidirectional()
                // .PageSize(50)
                .ReturnAll()
                .ResolveAsync(context => ResolveCountriesConnection(countryRepository, context));
        }

        private async static Task<object> ResolveCountriesConnection(
            ICountryRepository countryRepository,
            ResolveConnectionContext<object> context)
        {
            var first = context.First;
            var afterCode = Cursor.FromCursor<string>(context.After);
            var last = context.Last;
            var beforeCode = Cursor.FromCursor<string>(context.Before);
            var cancellationToken = context.CancellationToken;

            var getCountriesTask = GetCountries(countryRepository, first, afterCode, last, afterCode, cancellationToken);
            var getHasNextPageTask = GetHasNextPage(countryRepository, first, afterCode, cancellationToken);
            var getHasPreviousPageTask = GetHasPreviousPage(countryRepository, last, beforeCode, cancellationToken);
            var totalCountTask = countryRepository.GetTotalCount(cancellationToken);

            await Task.WhenAll(getCountriesTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask);
            var countries = getCountriesTask.Result;
            var hasNextPage = getHasNextPageTask.Result;
            var hasPreviousPage = getHasPreviousPageTask.Result;
            var totalCount = totalCountTask.Result;
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(countries, x => x.Code);

            return new Connection<Country>
            {
                Edges = countries
                    .Select(x =>
                        new Edge<Country>
                        {
                            Cursor = Cursor.ToCursor(x.Code),
                            Node = x
                        })
                    .ToList(),
                PageInfo = new PageInfo
                {
                    HasNextPage = hasNextPage,
                    HasPreviousPage = hasPreviousPage,
                    StartCursor = firstCursor,
                    EndCursor = lastCursor
                },
                TotalCount = totalCount
            };
        }

        private async static Task<Country[]> GetCountries(
            ICountryRepository countryRepository,
            int? first,
            string afterCode,
            int? last,
            string beforeCode,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await countryRepository.GetAll(first, afterCode, cancellationToken);
            }
            else if (last.HasValue)
            {
                return await countryRepository.GetAllReverse(last, beforeCode, cancellationToken);
            }

            return await countryRepository.GetAll(null, null, cancellationToken);
        }

        private async static Task<bool> GetHasNextPage(
            ICountryRepository countryRepository,
            int? first,
            string afterCode,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await countryRepository.GetHasNextPage(first, afterCode, cancellationToken);
            }
            else
            {
                return false;
            }
        }

        private async static Task<bool> GetHasPreviousPage(
            ICountryRepository countryRepository,
            int? last,
            string beforeCore,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await countryRepository.GetHasPreviousPage(last, beforeCore, cancellationToken);
            }
            else
            {
                return false;
            }
        }
    }
}
