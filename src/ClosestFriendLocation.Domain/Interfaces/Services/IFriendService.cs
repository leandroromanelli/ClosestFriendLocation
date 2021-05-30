using ClosestFriendLocation.Domain.Entities;
using System.Collections.Generic;

namespace ClosestFriendLocation.Domain.Interfaces.Services
{
    public interface IFriendService : IService<Friend>
    {
        List<Friend> GetClosestFriends(Location location);
    }
}
