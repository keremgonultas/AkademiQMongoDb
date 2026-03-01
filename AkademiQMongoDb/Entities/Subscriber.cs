using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    [BsonIgnoreExtraElements]
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
    }
}