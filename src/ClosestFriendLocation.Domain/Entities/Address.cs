namespace ClosestFriendLocation.Domain.Entities
{
    public class Address: BaseEntity
    {
        public Address() : base()
        {
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public Location Location { get; set; }
    }
}