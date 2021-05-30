namespace ClosestFriendLocation.Domain.Entities
{
    public class Location : BaseEntity
    {
        public Location() : base()
        {
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}