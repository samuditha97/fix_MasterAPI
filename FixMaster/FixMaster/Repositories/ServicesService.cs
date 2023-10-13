using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using MongoDB.Driver;

namespace FixMaster.Repositories
{
    public class ServicesService : IServiceInterface
    {
        private readonly IMongoCollection<ServiceClass> _serviceCollection;

        public ServicesService(IMongoDatabase database)
        {
            _serviceCollection = database.GetCollection<ServiceClass>("services");
        }

        public async Task CreateService(ServiceClass service)
        {
            service.id = IdGenerator.GenerateUniqueId(5);
            await _serviceCollection.InsertOneAsync(service);
        }

        public async Task UpdateService(string id, ServiceClass service)
        {
            var filter = Builders<ServiceClass>.Filter.Eq(s => s.id, id);
            var update = Builders<ServiceClass>.Update
                .Set(s => s.serviceName, service.serviceName)
                .Set(s => s.details, service.details);

            await _serviceCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteService(string id)
        {
            var filter = Builders<ServiceClass>.Filter.Eq(s => s.id, id);
            await _serviceCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<ServiceClass>> GetService()
        {
            var services = await _serviceCollection.Find(_ => true).ToListAsync();
            return services;
        }

        public async Task<ServiceClass> GetServiceById(string id)
        {
            var filter = Builders<ServiceClass>.Filter.Eq(s => s.id, id);
            var service = await _serviceCollection.Find(filter).FirstOrDefaultAsync();
            return service;
        }
    }
}

