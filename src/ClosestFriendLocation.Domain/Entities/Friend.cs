namespace ClosestFriendLocation.Domain.Entities
{
    public class Friend : BaseEntity
    {
        public Friend() : base()
        {
        }

        public string Name { get; set; }
        public Address Address { get; set; }

    }
}
