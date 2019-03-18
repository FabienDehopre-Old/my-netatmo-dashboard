using GraphQL;

namespace Netatmo.Dashboard.Api.Schema
{
    public class NetatmoSchema : GraphQL.Types.Schema
    {
        public NetatmoSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<NetatmoQuery>();
        }
    }
}
