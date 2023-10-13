using System;
using FixMaster.Interfaces;
using FixMaster.Models;
using MongoDB.Driver;

namespace FixMaster.Repositories
{
	public class BookingService : IBookingInterface
	{
        private readonly IMongoCollection<BookingClass> _bookingCollection;

        public BookingService(IMongoDatabase database)
        {
            _bookingCollection = database.GetCollection<BookingClass>("bookings");
        }

        public async Task CreateBooking(BookingClass booking)
        {
            booking.id = IdGenerator.GenerateUniqueId(5);
            await _bookingCollection.InsertOneAsync(booking);
        }

        public async Task UpdateBooking(string id, BookingClass booking)
        {
            var filter = Builders<BookingClass>.Filter.Eq(b => b.id, id);
            var update = Builders<BookingClass>.Update
                .Set(b => b.appoinmentDate, booking.appoinmentDate)
                .Set(b => b.requirement, booking.requirement)
                .Set(b => b.isCanceled, booking.isCanceled)
                .Set(b => b.technicianId, booking.technicianId)
                .Set(b => b.location, booking.location);

            await _bookingCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteBooking(string id)
        {
            var filter = Builders<BookingClass>.Filter.Eq(b => b.id, id);
            await _bookingCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<BookingClass>> GetAllBooking()
        {
            var bookings = await _bookingCollection.Find(_ => true).ToListAsync();
            return bookings;
        }

        public async Task<BookingClass> GetBookingById(string id)
        {
            var filter = Builders<BookingClass>.Filter.Eq(b => b.id, id);
            var booking = await _bookingCollection.Find(filter).FirstOrDefaultAsync();
            return booking;
        }
        public async Task<IEnumerable<BookingClass>> GetBookingsByCustomerId(string customerId)
        {
            var filter = Builders<BookingClass>.Filter.Eq(b => b.customerId, customerId);
            var bookings = await _bookingCollection.Find(filter).ToListAsync();
            return bookings;
        }

    }
}

