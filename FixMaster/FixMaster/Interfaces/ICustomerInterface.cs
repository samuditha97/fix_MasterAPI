using System;
using FixMaster.Models;

namespace FixMaster.Interfaces
{
	public interface ICustomerInterface
	{
        Task CreateCustomer(CustomerClass customer);
        Task UpdateCustomer(string id, CustomerClass customer);
        Task DeleteCustomer(string id);
        Task<IEnumerable<CustomerClass>> GetCustomers();
        Task<CustomerClass> GetCustomerById(string id);
        Task<CustomerClass> LoginCustomerAsync(string email);

      
    }
}

