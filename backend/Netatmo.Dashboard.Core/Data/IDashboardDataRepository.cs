using Netatmo.Dashboard.Core.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Core.Data
{
    public interface IDashboardDataRepository
    {
        Task<DashboardData[]> GetAll(string deviceId);
    }
}
