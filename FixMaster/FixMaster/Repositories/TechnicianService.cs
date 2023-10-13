using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using MongoDB.Driver;

namespace FixMaster.Repositories
{
    public class ITechnicianService : ITechnicianInterface
    {
        private readonly IMongoCollection<TechnicianClass> _technicianCollection;

        public ITechnicianService(IMongoDatabase database)
        {
            _technicianCollection = database.GetCollection<TechnicianClass>("technicians");
        }

        public async Task CreateTechnician(TechnicianClass technician)
        {
            technician.id = IdGenerator.GenerateUniqueId(5);
            await _technicianCollection.InsertOneAsync(technician);
        }

        public async Task UpdateTechnician(string id, TechnicianClass technician)
        {
            var filter = Builders<TechnicianClass>.Filter.Eq(t => t.id, id);
            var update = Builders<TechnicianClass>.Update
                .Set(t => t.Name, technician.Name)
                .Set(t => t.mobileNo, technician.mobileNo)
                .Set(t => t.serviceId, technician.serviceId)
                .Set(t => t.review, technician.review)
                .Set(t => t.rate, technician.rate)
                .Set(t => t.location, technician.location);

            await _technicianCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteTechnician(string id)
        {
            var filter = Builders<TechnicianClass>.Filter.Eq(t => t.id, id);
            await _technicianCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<TechnicianClass>> GetTechnician()
        {
            var technicians = await _technicianCollection.Find(_ => true).ToListAsync();
            return technicians;
        }

        public async Task<TechnicianClass> GetTechnicianById(string id)
        {
            var filter = Builders<TechnicianClass>.Filter.Eq(t => t.id, id);
            var technician = await _technicianCollection.Find(filter).FirstOrDefaultAsync();
            return technician;
        }

        public async Task<IEnumerable<TechnicianClass>> GetTechniciansByServiceId(string serviceId)
        {
            var filter = Builders<TechnicianClass>.Filter.Eq(t => t.serviceId, serviceId);
            var technicians = await _technicianCollection.Find(filter).ToListAsync();
            return technicians;
        }
    }
}

