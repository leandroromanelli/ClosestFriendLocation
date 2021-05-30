using ClosestFriendLocation.Domain.Entities;
using System.Collections.Generic;

namespace ClosestFriendLocation.Domain.Repositories
{
    public interface IFriendRepository : IRepository<Friend>
    {
        List<Friend> GetComplete();
    }
}
