using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities 
{
    [BsonIgnoreExtraElements]
    public class Chef : BaseEntity
    {
        public string Name { get; set; } 
        public string Title { get; set; } 
        public string ImageUrl { get; set; } 
        public bool Status { get; set; } 
    }
}