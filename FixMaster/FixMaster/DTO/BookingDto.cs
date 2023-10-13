using System;
namespace FixMaster.DTO
{
	public class BookingDto
	{
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string ServiceName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Requirement { get; set; }
        public string IsCanceled { get; set; }
        public string TechnicianId { get; set; }
        public string Location { get; set; }
    }
}

