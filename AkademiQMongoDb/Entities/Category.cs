using AkademiQMongoDb.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkademiQMongoDb.Entities
{
    public class Category : BaseEntity
    {   
        public string Name { get; set; }
        public string ImageUrl { get; set; } // Sadece Slider'da gözükmesi için eklendi!
        public bool Status { get; set; }
    }
}
