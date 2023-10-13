using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using FixMaster.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FixMaster.Controllers
{
    [Route("api/technicians")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianInterface _technicianService;

        public TechnicianController(ITechnicianInterface technicianService)
        {
            _technicianService = technicianService;
        }

        // POST api/technicians
        [HttpPost]
        public async Task<IActionResult> CreateTechnician([FromBody] TechnicianClass technician)
        {
            await _technicianService.CreateTechnician(technician);
            return Ok(technician);
        }

        // PUT api/technicians/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnician(string id, [FromBody] TechnicianClass technician)
        {
            await _technicianService.UpdateTechnician(id, technician);
            return Ok(technician);
        }

        // DELETE api/technicians/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnician(string id)
        {
            await _technicianService.DeleteTechnician(id);
            return Ok();
        }

        // GET api/technicians
        [HttpGet]
        public async Task<IActionResult> GetTechnicians()
        {
            var technicians = await _technicianService.GetTechnician();
            return Ok(technicians);
        }

        // GET api/technicians/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechnicianById(string id)
        {
            var technician = await _technicianService.GetTechnicianById(id);
            if (technician == null)
            {
                return NotFound();
            }

            return Ok(technician);
        }

        // GET api/technicians/byservice/{serviceId}
        [HttpGet("byservice/{serviceId}")]
        public async Task<IActionResult> GetTechniciansByServiceId(string serviceId)
        {
            var technicians = await _technicianService.GetTechniciansByServiceId(serviceId);
            return Ok(technicians);
        }

    }
}

