using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Models
{
    public class Sugar
    {
        [BsonElement("count")]
        public int Count { get; set; }

        [BsonElement("type")]
        public int Type { get; set; }
    }
}
