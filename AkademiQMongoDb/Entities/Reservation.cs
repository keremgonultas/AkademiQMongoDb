using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    [BsonIgnoreExtraElements]
    public class Reservation : BaseEntity
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; } 
        public string PersonCount { get; set; } 
        public string SpecialRequest { get; set; } 
        public string Status { get; set; } 
    }
}