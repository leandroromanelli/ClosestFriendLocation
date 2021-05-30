using Newtonsoft.Json;

namespace ClosestFriendLocation.Api.Models
{
    public class FriendModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public AddressModel Address { get; set; }

    }
}
