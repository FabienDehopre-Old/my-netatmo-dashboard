using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Persistence.Infrastructure;

namespace Netatmo.Dashboard.Persistence
{
    public class NetatmoDbContextFactory : DesignTimeDbContextFactoryBase<NetatmoDbContext>
    {
        protected override NetatmoDbContext CreateNewInstance(DbContextOptions<NetatmoDbContext> options)
        {
            return new NetatmoDbContext(options);
        }
    }
}
