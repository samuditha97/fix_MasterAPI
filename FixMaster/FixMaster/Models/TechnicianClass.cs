using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixMaster.Models
{
	public class TechnicianClass
	{
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public string id { get; set; }
        public string Name { get; set; }
        public string mobileNo { get; set; }
        public string serviceId { get; set; }
        public List<Review> review { get; set; }
        public Rate rate { get; set;  }
        public string location { get; set; }

    }

    public class Review
    {
        public string message { get; set; }
        public DateTime date { get; set; }
    }

    public enum Rate
    {
        OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
    }
}

