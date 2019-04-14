using GraphQL.Types;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class DashboardDataUnion : UnionGraphType
    {
        public DashboardDataUnion()
        {
            //Type<MainDashboardDataType>();
            Type<OutdoorDashboardDataObject>();
            Type<WindGaugeDashboardDataObject>();
            Type<RainGaugeDashboardDataObject>();
            Type<IndoorDashboardDataObject>();
        }
    }
}
