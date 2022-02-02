using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    public class FactoryDevicesController : Controller
    {
        private readonly IFactoryDeviceService _factoryDeviceService;
        private readonly IMaintenanceTaskService _maintenanceTaskService;

        public FactoryDevicesController(IFactoryDeviceService factoryDeviceService, IMaintenanceTaskService maintenanceTaskService)
        {
            _factoryDeviceService = factoryDeviceService;
            _maintenanceTaskService = maintenanceTaskService;
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<FactoryDeviceDto>> Get()
        {
            return (await _factoryDeviceService.GetAll()).Select(ModelToDto);
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var fd = await _factoryDeviceService.Get(id);
            if (fd == null)
            {
                return NotFound();
            }

            return Ok(ModelToDto(fd));
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/1/tasks
        /// </summary>
        [HttpGet("{id}/tasks")]
        public async Task<IEnumerable<MaintenanceTaskDto>> GetTasksByDevice(int id)
        {
            var tasks = await _maintenanceTaskService.GetByDevice(id);

            return tasks.Select(mt => new MaintenanceTaskDto
            {
                Id = mt.Id,
                DeviceId = mt.DeviceId,
                IssueDate = mt.IssueDate,
                Description = mt.Description,
                Importance = mt.Importance,
                Closed = mt.Closed
            });
        }
        
        private static FactoryDeviceDto ModelToDto(FactoryDevice fd)
        {
            return new FactoryDeviceDto
            {
                Id = fd.Id,
                Name = fd.Name,
                Year = fd.Year,
                Type = fd.Type
            };
        }
    }
}