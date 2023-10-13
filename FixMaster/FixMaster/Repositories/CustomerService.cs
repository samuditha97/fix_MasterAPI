using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using MongoDB.Driver;

namespace FixMaster.Repositories
{
	public class CustomerService : ICustomerInterface
	{
        private readonly IMongoCollection<CustomerClass> _customerCollection;

        public CustomerService(IMongoDatabase database)
        {
            _customerCollection = database.GetCollection<CustomerClass>("customers");
        }

        public async Task CreateCustomer(CustomerClass customer)
        {
            customer.id = IdGenerator.GenerateUniqueId(5);
            await _customerCollection.InsertOneAsync(customer);
        }

        public async Task UpdateCustomer(string id, CustomerClass customer)
        {
            var filter = Builders<CustomerClass>.Filter.Eq(c => c.id, id);
            var update = Builders<CustomerClass>.Update
                .Set(c => c.firstName, customer.firstName)
                .Set(c => c.lastName, customer.lastName)
                .Set(c => c.mobileNo, customer.mobileNo)
                .Set(c => c.address, customer.address)
                .Set(c => c.email, customer.email);

            await _customerCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteCustomer(string id)
        {
            var filter = Builders<CustomerClass>.Filter.Eq(c => c.id, id);
            await _customerCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<CustomerClass>> GetCustomers()
        {
            var customers = await _customerCollection.Find(_ => true).ToListAsync();
            return customers;
        }

        public async Task<CustomerClass> GetCustomerById(string id)
        {
            var filter = Builders<CustomerClass>.Filter.Eq(c => c.id, id);
            var customer = await _customerCollection.Find(filter).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<CustomerClass> LoginCustomerAsync(string email)
        {
            var customer = await _customerCollection.Find(c => c.email == email).FirstOrDefaultAsync();

            return customer;
        }

    }
}

