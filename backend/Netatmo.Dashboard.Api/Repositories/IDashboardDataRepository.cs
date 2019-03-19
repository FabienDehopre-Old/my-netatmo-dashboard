using Netatmo.Dashboard.Api.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public interface IDashboardDataRepository
    {
        Task<DashboardData[]> GetAll(string deviceId);
    }
}
