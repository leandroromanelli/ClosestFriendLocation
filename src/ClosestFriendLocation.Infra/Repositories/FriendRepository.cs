using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Repositories;
using ClosestFriendLocation.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ClosestFriendLocation.Infra.Repositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {

        ClosestFriendLocationContext _context;
        public FriendRepository(ClosestFriendLocationContext context) : base(context)
        {
            _context = context;
        }

        public List<Friend> GetComplete()
        {
            return _context.Set<Friend>()
                .Include("Address")
                .Include("Address.Location")
                .ToList();
        }
    }
}
