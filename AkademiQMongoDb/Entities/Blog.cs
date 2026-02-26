using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    [BsonIgnoreExtraElements]
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; }
        public string Author { get; set; } 
        public string Date { get; set; }
    }
}
