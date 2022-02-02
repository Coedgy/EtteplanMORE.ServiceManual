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
        private readonly IFactoryDeviceService _factoryDeviceService;

        public MaintenanceTasksController(IMaintenanceTaskService maintenanceTaskService, IFactoryDeviceService factoryDeviceService)
        {
            _maintenanceTaskService = maintenanceTaskService;
            _factoryDeviceService = factoryDeviceService;
        }
        
        //HTTP GET: api/maintenancetasks/
        [HttpGet]
        public async Task<IEnumerable<MaintenanceTaskDto>> GetTasks()
        {
            var tasks = await _maintenanceTaskService.GetAll();

            return tasks?.Select(ModelToDto);
        }
        
        //HTTP GET: api/maintenancetasks/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var mt = await _maintenanceTaskService.Get(id);
            if (mt == null)
            {
                return NotFound();
            }
            
            return Ok(ModelToDto(mt));
        }
        
        //HTTP GET: api/maintenancetasks/search?variables
        [HttpGet("search")]
        public async Task<IEnumerable<MaintenanceTaskDto>> SearchTasks(MaintenanceTaskSearch variables)
        {
            var tasks = await _maintenanceTaskService.Search(variables);
            
            return tasks?.Select(ModelToDto);
        }

        //HTTP GET: api/maintenancetasks/device/1
        [HttpGet("device/{deviceId}")]
        public async Task<IEnumerable<MaintenanceTaskDto>> GetByDevice(int deviceId)
        {
            var tasks = await _maintenanceTaskService.GetByDevice(deviceId);
            
            return tasks?.Select(ModelToDto);
        }

        //HTTP POST: api/maintenancetasks?variables
        [HttpPost]
        public async Task<IActionResult> PostTask(MaintenanceTaskDto taskDto)
        {
            if (taskDto.DeviceId == 0)
            {
                return BadRequest("Device ID is not optional, and it cannot be less than 1!");
            }

            if (await _factoryDeviceService.Get(taskDto.DeviceId) == null)
            {
                return BadRequest($"No device found with the ID '{ taskDto.DeviceId }'");
            }
            
            if (taskDto.Importance < 0 || taskDto.Importance > ImportanceLevel.Critical)
            {
                return BadRequest("Incorrect importance value");
            }

            var task = DtoToModel(taskDto);
            await _maintenanceTaskService.PostTask(task);
            return Ok("New maintenance task created.");
        }
        
        //HTTP PUT: api/maintenancetasks/1/close
        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseTask(int id)
        {
            var task = await _maintenanceTaskService.Get(id);
            task.Closed = true;
            await _maintenanceTaskService.UpdateTask(task);

            return Ok("Task status set to closed.");
        }

        //HTTP PUT: api/maintenancetasks/1?variables
        [HttpPut]
        public async Task<IActionResult> UpdateTask(MaintenanceTaskDto taskDto)
        {
            if (taskDto.Id == 0)
            {
                return BadRequest("ID is not optional, and it cannot be less than 1!");
            }

            if (_maintenanceTaskService.Get(taskDto.Id) == null)
            {
                return BadRequest($"No task found with the id {taskDto.Id}");
            }
            
            if (taskDto.DeviceId != 0 && _factoryDeviceService.Get(taskDto.DeviceId) == null)
            {
                return BadRequest($"No device found with the given id { taskDto.DeviceId }");
            }
            
            if (taskDto.Importance < 0 || taskDto.Importance > ImportanceLevel.Critical)
            {
                return BadRequest("Incorrect importance value");
            }
            
            var task = DtoToModel(taskDto);
            await _maintenanceTaskService.UpdateTask(task);
            return Ok($"Task ID { task.Id } was updated successfully.");
        }

        //HTTP DELETE: api/maintenancetasks/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (await _maintenanceTaskService.Get(id) == null)
            {
                return BadRequest($"No task found with the ID '{ id }'");
            }
            
            await _maintenanceTaskService.DeleteTask(id);
            return Ok($"Task with ID { id } was deleted successfully.");
        }
        
        private static MaintenanceTaskDto ModelToDto(MaintenanceTask mt)
        {
            return new MaintenanceTaskDto
            {
                Id = mt.Id,
                DeviceId = mt.DeviceId,
                IssueDate = mt.IssueDate,
                Description = mt.Description,
                Importance = mt.Importance,
                Closed = mt.Closed
            };
        }

        private static MaintenanceTask DtoToModel(MaintenanceTaskDto dto)
        {
            MaintenanceTask newTask = new MaintenanceTask
            {
                Id = dto.Id,
                DeviceId = dto.DeviceId,
                IssueDate = dto.IssueDate,
                Description = dto.Description,
                Importance = dto.Importance,
                Closed = dto.Closed
            };

            return newTask;
        }
    }
}