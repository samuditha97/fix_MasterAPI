using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixMaster.Models
{
	public class ServiceClass
	{
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string id { get; set; }
        public string serviceName { get; set; }
        public string details { get; set; }

    }
}

