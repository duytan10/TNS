using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace TNS.Models
{
    public enum Status
    {
        Available, SoldOut, Reserve
    }

    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("short_description")]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("discount_percent")]
        [DisplayName("Discount Percent")]
        public byte DiscountPercent { get; set; }

        [BsonRepresentation(BsonType.String), BsonElement("status")]
        public Status Status { get; set; }

        [BsonElement("details")]
        public string Details { get; set; }

        [BsonElement("guagantee_month")]
        [DisplayName("Guagantee Month")]
        public byte GuaganteeMonth { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}