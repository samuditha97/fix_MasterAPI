using System;
using FixMaster.Models;

namespace FixMaster.Interfaces
{
	public interface IServiceInterface
	{
        Task CreateService(ServiceClass service);
        Task UpdateService(string id, ServiceClass service);
        Task DeleteService(string id);
        Task<IEnumerable<ServiceClass>> GetService();
        Task<ServiceClass> GetServiceById(string id);
    }
}

