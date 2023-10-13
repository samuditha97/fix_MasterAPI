using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using FixMaster.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FixMaster.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceInterface _serviceService;

        public ServicesController(IServiceInterface serviceService)
        {
            _serviceService = serviceService;
        }

        // POST api/services
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceClass service)
        {
            await _serviceService.CreateService(service);
            return Ok(service);
        }

        // PUT api/services/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(string id, [FromBody] ServiceClass service)
        {
            await _serviceService.UpdateService(id, service);
            return Ok(service);
        }

        // DELETE api/services/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(string id)
        {
            await _serviceService.DeleteService(id);
            return Ok();
        }

        // GET api/services
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetService();
            return Ok(services);
        }

        // GET api/services/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(string id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
    }
}

