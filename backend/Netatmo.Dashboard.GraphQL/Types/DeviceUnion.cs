using GraphQL.Types;

namespace Netatmo.Dashboard.GraphQL.Types
{
    public class DeviceUnion : UnionGraphType
    {
        public DeviceUnion()
        {
            Type<MainDeviceObject>();
            Type<ModuleDeviceObject>();
        }
    }
}
