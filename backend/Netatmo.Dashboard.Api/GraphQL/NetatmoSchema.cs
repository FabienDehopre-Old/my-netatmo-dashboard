using GraphQL;
using GraphQL.Types;

namespace Netatmo.Dashboard.Api.GraphQL
{
    public class NetatmoSchema : Schema
    {
        public NetatmoSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<NetatmoQuery>();
        }
    }
}
