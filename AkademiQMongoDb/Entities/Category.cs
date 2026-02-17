using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    public class Category : BaseEntity
    {   
        public string Name { get; set; }
    }
}
