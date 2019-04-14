using GraphQL.Types;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class ModuleDeviceTypeEnumeration : EnumerationGraphType<Core.Models.ModuleDeviceType>
    {
        public ModuleDeviceTypeEnumeration()
        {
            Name = "ModuleDeviceType";
            Description = "Defines which type of module it is";
        }
    }
}
