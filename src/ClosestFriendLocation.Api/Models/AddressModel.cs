using Newtonsoft.Json;

namespace ClosestFriendLocation.Api.Models
{
    public class AddressModel
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("location")]
        public LocationModel Location { get; set; }
    }
}