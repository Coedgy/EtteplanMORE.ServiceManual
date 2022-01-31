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

        public FactoryDevicesController(IFactoryDeviceService factoryDeviceService)
        {
            _factoryDeviceService = factoryDeviceService;
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