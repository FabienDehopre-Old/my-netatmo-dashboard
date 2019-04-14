using GraphQL;
using GraphQL.Types;
using Netatmo.Dashboard.GraphQL.Types;

namespace Netatmo.Dashboard.GraphQL.Schemas
{
    public class MainSchema : Schema
    {
        public MainSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<QueryObject>();
            RegisterType<MainDeviceObject>();
            RegisterType<ModuleDeviceObject>();
            RegisterType<MainDashboardDataObject>();
            RegisterType<OutdoorDashboardDataObject>();
            RegisterType<WindGaugeDashboardDataObject>();
            RegisterType<RainGaugeDashboardDataObject>();
            RegisterType<IndoorDashboardDataObject>();
        }
    }
}
