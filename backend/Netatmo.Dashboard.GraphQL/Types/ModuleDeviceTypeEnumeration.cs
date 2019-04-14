using GraphQL.Types;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class ModuleDeviceTypeEnumeration : EnumerationGraphType<ModuleDeviceType>
    {
        public ModuleDeviceTypeEnumeration()
        {
            Name = "ModuleDeviceType";
            Description = "Defines which type of module it is";

            AddValue("INDOOR", "Additional indoor module", ModuleDeviceType.Indoor);
            AddValue("OUTDOOR", "Measure the outdoor environment", ModuleDeviceType.Outdoor);
            AddValue("RAINGAUGE", "Measure the rain quantity", ModuleDeviceType.RainGauge);
            AddValue("WINDGAUGE", "Measure the wind power", ModuleDeviceType.WindGauge);
        }
    }
}
