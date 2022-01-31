using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IMaintenanceTaskService
    {
        Task<IEnumerable<MaintenanceTask>> GetAll();

        Task<MaintenanceTask> Get(int id);
        
        //SEARCH

        Task<IEnumerable<MaintenanceTask>> GetByDevice(int deviceId);

        Task<IAsyncResult> PostTask(MaintenanceTask task);

        Task<IAsyncResult> UpdateTask(MaintenanceTask updatedTask);

        Task<IAsyncResult> DeleteTask(int id);
    }
}