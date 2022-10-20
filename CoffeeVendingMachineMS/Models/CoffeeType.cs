using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoffeeVendingMachineMS.Models
{
    public class CoffeeType
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("code")]
        public int Code { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("sugar")]
        public Sugar Sugar { get; set; }

        [BsonElement("milk_dose")]
        public int MilkDose { get; set; }

        [BsonElement("cream")]
        public bool Cream { get; set; }

        [BsonElement("caramelle")]
        public bool Caramelle { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
