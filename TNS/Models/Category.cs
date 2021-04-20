using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TNS.Models
{
    public class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("product_id")]
        public List<ObjectId> ProductId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("iamge")]
        public string Image { get; set; }
    }
}