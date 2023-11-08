using System;
using FixMaster.DTO;
using FixMaster.Interfaces;
using FixMaster.Models;
using Microsoft.AspNetCore.Mvc;

namespace FixMaster.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
	{
        private readonly IBookingInterface _bookingService;

        public BookingController(IBookingInterface bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingClass booking)
        {
            await _bookingService.CreateBooking(booking);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingClass>>> GetAllBooking()
        {
            var bookings = await _bookingService.GetAllBooking();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingClass>> GetBookingById(string id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookingField(string id, [FromBody] BookingClass updateModel)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Update only the fields that are provided in the update model
            //test comment
            if (!string.IsNullOrEmpty(updateModel.serviceName))
            {
                booking.serviceName = updateModel.serviceName;
            }

            if (updateModel.appoinmentDate != null)
            {
                booking.appoinmentDate = updateModel.appoinmentDate;
            }

            if (!string.IsNullOrEmpty(updateModel.requirement))
            {
                booking.requirement = updateModel.requirement;
            }

            if (updateModel.isCanceled != null)
            {
                booking.isCanceled = updateModel.isCanceled;
            }

            if (!string.IsNullOrEmpty(updateModel.technicianId))
            {
                booking.technicianId = updateModel.technicianId;
            }

            if (!string.IsNullOrEmpty(updateModel.location))
            {
                booking.location = updateModel.location;
            }

            await _bookingService.UpdateBooking(id, booking);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            await _bookingService.DeleteBooking(id);
            return Ok();
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByCustomerId(string customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerId(customerId);

            var bookingDtos = bookings.Select(booking => new BookingDto
            {
                Id = booking.id,
                CustomerId = booking.customerId,
                ServiceName = booking.serviceName,
                AppointmentDate = booking.appoinmentDate,
                Requirement = booking.requirement,
                IsCanceled = booking.isCanceled ? "Canceled" : "Not Canceled", // Format IsCanceled property
                TechnicianId = booking.technicianId,
                Location = booking.location
            }).ToList();

            return Ok(bookingDtos);
        }

    }
}

