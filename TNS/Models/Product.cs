using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace TNS.Models
{
    public enum Status
    {
        Available, SoldOut, Reserve
    }

    public enum ProductDetailType
    {
        Text, Image
    }

    public class Details
    {
        [BsonRepresentation(BsonType.String), BsonElement("type")]
        public ProductDetailType Type;

        [BsonElement("content")]
        public string Content;
    }

    public class Product
    {
        public static Dictionary<string, string> StatusType = new Dictionary<string, string>()
        {
            {"Available", "Còn hàng" },
            {"SoldOut", "Hết hàng" },
            {"Reserve", "Đặt trước" }
        };

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("short_descriptions")]
        [DisplayName("Short Descriptions")]
        public List<string> ShortDescriptions { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("discount_percent")]
        [DisplayName("Discount Percent")]
        public byte DiscountPercent { get; set; }

        [BsonRepresentation(BsonType.String), BsonElement("status")]
        public Status Status { get; set; }

        [BsonElement("details")]
        public List<Details> Details { get; set; }

        [BsonElement("guarantee_month")]
        [DisplayName("Guarantee Month")]
        public byte GuaranteeMonth { get; set; }

        [BsonElement("images")]
        public List<string> Images { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("in_stock")]
        [DisplayName("In Stock")]
        public int InStock { get; set; }
    }
}