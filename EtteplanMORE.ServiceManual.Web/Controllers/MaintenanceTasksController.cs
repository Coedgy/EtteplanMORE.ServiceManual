using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    public class MaintenanceTasksController : Controller
    {
        private readonly IMaintenanceTaskService _maintenanceTaskService;

        public MaintenanceTasksController(IMaintenanceTaskService maintenanceTaskService)
        {
            _maintenanceTaskService = maintenanceTaskService;
        }
        
        //HTTP GET: api/maintenancetasks/
        [HttpGet]
        public async Task<IEnumerable<MaintenanceTaskDto>> Get()
        {
            return (await _maintenanceTaskService.GetAll())
                .Select(mt => 
                    new MaintenanceTaskDto {
                        Id = mt.Id,
                        Device = mt.Device,
                        IssueDate = mt.IssueDate,
                        Description = mt.Description,
                        Importance = mt.Importance,
                        Closed = mt.Closed
                    }
                );
        }
        
        //HTTP GET: api/maintenancetasks/1
        
        
        //HTTP GET: api/maintenancetasks/device/1
        
        
        //HTTP GET: api/maintenancetasks/search?
        
        
        //HTTP POST: api/maintenancetasks?
        
        
        //HTTP PUT: api/maintenancetasks/1/close
        
        
        //HTTP PUT: api/maintenancetasks/1?
        
        
        //HTTP DELETE: api/maintenancetasks/1
        
    }
}