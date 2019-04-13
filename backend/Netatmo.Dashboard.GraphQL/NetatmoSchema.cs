using GraphQL;
using GraphQL.Types;
using Netatmo.Dashboard.GraphQL;

namespace Netatmo.Dashboard.GraphQL
{
    public class NetatmoSchema : Schema
    {
        public NetatmoSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<NetatmoQuery>();
            RegisterType<MainDeviceType>();
            RegisterType<ModuleDeviceType>();
            RegisterType<MainDashboardDataType>();
            RegisterType<OutdoorDashboardDataType>();
            RegisterType<WindGaugeDashboardDataType>();
            RegisterType<RainGaugeDashboardDataType>();
            RegisterType<IndoorDashboardDataType>();
        }
    }
}
