using System;
using FixMaster.Models;

namespace FixMaster.Interfaces
{
	public interface ITechnicianInterface
	{
        Task CreateTechnician(TechnicianClass technician);
        Task UpdateTechnician(string id, TechnicianClass technician);
        Task DeleteTechnician(string id);
        Task<IEnumerable<TechnicianClass>> GetTechnician();
        Task<TechnicianClass> GetTechnicianById(string id);
        Task<IEnumerable<TechnicianClass>> GetTechniciansByServiceId(string serviceId);
    }
}

