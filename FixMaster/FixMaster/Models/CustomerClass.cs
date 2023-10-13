using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixMaster.Models
{
	public class CustomerClass
	{
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string id { get; set; }
        public string firstName { get; set; }
		public string lastName { get; set; }
		public string mobileNo { get; set; }
		public string address { get; set; }
		public string email { get; set; }
	}
}

