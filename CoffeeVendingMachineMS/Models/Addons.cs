using Newtonsoft.Json;

namespace CoffeeVendingMachineMS.Models
{
    public class Addons
    {
        [JsonProperty("sugar")]
        public AddonInfo Sugar { get; set; }

        [JsonProperty("milk")]
        public AddonInfo Milk { get; set; }

        [JsonProperty("cream")]
        public AddonInfo Cream { get; set; }

        [JsonProperty("caramelle")]
        public AddonInfo Caramelle { get; set; }
    }

    public class AddonInfo
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
