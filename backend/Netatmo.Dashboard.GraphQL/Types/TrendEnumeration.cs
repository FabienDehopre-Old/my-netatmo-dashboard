using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class TrendEnumeration : EnumerationGraphType<Trend>
    {
        public TrendEnumeration()
        {
            Name = "Trend";
            Description = "Which is the trend of the measure over time";
        }
    }
}
