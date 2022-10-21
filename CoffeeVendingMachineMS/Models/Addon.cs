using Newtonsoft.Json;

namespace CoffeeVendingMachineMS.Models
{
    public class Addon
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
