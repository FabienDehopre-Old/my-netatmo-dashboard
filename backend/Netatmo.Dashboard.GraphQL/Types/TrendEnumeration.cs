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

            AddValue("UP", "The pressure or temperature has increased since the last measure", Trend.Up);
            AddValue("DOWN", "The pressure or temperature has decreased since the last measure", Trend.Down);
            AddValue("STABLE", "The pressure or temperature did remain the same since the last measure", Trend.Stable);
        }
    }
}
