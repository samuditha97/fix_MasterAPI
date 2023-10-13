using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixMaster.Models
{
	public class BookingClass
	{
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string id { get; set; }
        public string customerId { get; set; }
        public string serviceName { get; set; }
        public DateTime appoinmentDate { get; set; }
        public string requirement { get; set; }
        public bool isCanceled { get; set; } = false;
        public string technicianId { get; set; }
        public string location { get; set; }
    }
}

