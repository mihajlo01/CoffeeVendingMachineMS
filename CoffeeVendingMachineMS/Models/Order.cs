using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Models
{
    public class Order
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("coffee_type")]
        public CoffeeType CoffeeType { get; set; }

        [BsonElement("inserted_cash")]
        public decimal InsertedCash { get; set; }

        [BsonElement("inserted_cash")]
        public decimal ReturnedCash { get; set; }

        [BsonElement("order_time")]
        public DateTime OrderTime { get; set; }
    }
}
