using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    [BsonIgnoreExtraElements]
    public class Testimonial : BaseEntity
    {
        public string Title { get; set; } 
        public string Comment { get; set; } 
        public string ImageUrl { get; set; } 
        public string FoodImageUrl { get; set; } 
        public bool Status { get; set; } 
    }
}