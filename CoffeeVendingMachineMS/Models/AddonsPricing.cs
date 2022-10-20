using Newtonsoft.Json;

namespace CoffeeVendingMachineMS.Models
{
    public class AddonsPricing
    {
        [JsonProperty("milk")]
        public decimal Milk { get; set; }

        [JsonProperty("cream")]
        public decimal Cream { get; set; }

        [JsonProperty("caramelle")]
        public decimal Caramelle { get; set; }
    }
}
