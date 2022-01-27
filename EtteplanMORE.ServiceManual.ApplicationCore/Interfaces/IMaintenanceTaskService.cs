using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IMaintenanceTaskService
    {
        Task<IEnumerable<MaintenanceTask>> GetAll();

        Task<MaintenanceTask> Get(int id);
    }
}