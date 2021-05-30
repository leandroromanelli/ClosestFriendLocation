using ClosestFriendLocation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClosestFriendLocation.Infra.Context
{
    public class ClosestFriendLocationContext : DbContext
    {
        public ClosestFriendLocationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
