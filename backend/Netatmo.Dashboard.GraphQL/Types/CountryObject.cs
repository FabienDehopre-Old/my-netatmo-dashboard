using GraphQL.Types;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class CountryObject : ObjectGraphType<Country>
    {
        public CountryObject(IStationRepository stationRepository)
        {
            Name = "Country";
            Description = "A country is a region that is identified as a distinct entity in political geography.";

            Field(x => x.Code)
                .Type(new NonNullGraphType(new IdGraphType()))
                .Description("The ISO 3166-1 alpha-2 code representing the country.");
            Field(x => x.Flag)
                .Description("The URL of the flag of the country.");
            Field(x => x.NameEN)
                .Description("The English name of the country.");
            Field(x => x.NameBR)
                .Description("The Brazilian name of the country.");
            Field(x => x.NamePT)
                .Description("The Portuguese name of the country.");
            Field(x => x.NameNL)
                .Description("The Dutch name of the country.");
            Field(x => x.NameHR)
                .Description("The Hungarian name of the country.");
            Field(x => x.NameFA)
                .Description("The Farsi name of the country.");
            Field(x => x.NameDE)
                .Description("The German name of the country.");
            Field(x => x.NameES)
                .Description("The Spanish name of the country.");
            Field(x => x.NameFR)
                .Description("The French name of the country.");
            Field(x => x.NameJA)
                .Description("The Japanese name of the country.");
            Field(x => x.NameIT)
                .Description("The italian name of the country.");
            //Connection<StationObject>()
            //    .Name("stations")
            //    .Description("The weather stations located in the country.")
            //    .Bidirectional()
            //    .PageSize(50)
            //    .ResolveAsync(context => ResolveConnection(stationRepository, context));
        }

        /*private async static Task<object> ResolveConnection(
            IStationRepository stationRepository,
            ResolveConnectionContext<Country> context)
        {
            var first = context.First;
            var afterCursor = Cursor.FromCursor<int?>(context.After);
            var last = context.Last;
            var beforeCursor = Cursor.FromCursor<int?>(context.Before);

            var getAllTask = GetStations(stationRepository, context.Source.Code, first, afterCursor, last, beforeCursor, context.CancellationToken);
            var getHasNextPageTask = GetHasNextPage(stationRepository, context.Source.Code, first, afterCursor, context.CancellationToken);
            var getHasPreviousPageTask = GetHasPreviousPage(stationRepository, context.Source.Code, last, beforeCursor, context.CancellationToken);
            var totalCountTask = stationRepository.GetTotalCount(context.Source.Code, context.CancellationToken);

            await Task.WhenAll(getAllTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask);
            var stations = getAllTask.Result;
            var hasNextPage = getHasNextPageTask.Result;
            var hasPreviousPage = getHasPreviousPageTask.Result;
            var totalCount = totalCountTask.Result;
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(stations, x => x.Id);

            return new Connection<Station>
            {
                Edges = stations.Select(x => new Edge<Station>
                {
                    Cursor = Cursor.ToCursor(x.Id),
                    Node = x
                }).ToList(),
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

        private async static Task<Station[]> GetStations(
            IStationRepository stationRepository,
            string countryCode,
            int? first,
            int? afterCursor,
            int? last,
            int? beforeCursor,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await stationRepository.GetAllReverse(countryCode, last, beforeCursor, cancellationToken);
            }
            else
            {
                return await stationRepository.GetAll(countryCode, first, afterCursor, cancellationToken);
            }
        }

        private async static Task<bool> GetHasNextPage(
            IStationRepository stationRepository,
            string countryCode,
            int? first,
            int? afterCursor,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await stationRepository.GetHasNextPage(countryCode, first, afterCursor, cancellationToken);
            }
            else
            {
                return false;
            }
        }

        private async static Task<bool> GetHasPreviousPage(
            IStationRepository stationRepository,
            string countryCode,
            int? last,
            int? beforeCursor,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await stationRepository.GetHasPreviousPage(countryCode, last, beforeCursor, cancellationToken);
            }
            else
            {
                return false;
            }
        }*/
    }
}
