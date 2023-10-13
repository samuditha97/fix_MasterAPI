using System;
using FixMaster.Models;

namespace FixMaster.Interfaces
{
	public interface IBookingInterface
	{
		Task CreateBooking(BookingClass booking);
		Task UpdateBooking(string id, BookingClass booking);
		Task DeleteBooking(string id);
		Task<IEnumerable<BookingClass>> GetAllBooking();
		Task<BookingClass> GetBookingById(string id);
        Task<IEnumerable<BookingClass>> GetBookingsByCustomerId(string customerId);

    }
}

