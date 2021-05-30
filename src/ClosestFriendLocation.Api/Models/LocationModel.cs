using Newtonsoft.Json;

namespace ClosestFriendLocation.Api.Models
{
    public class LocationModel
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}