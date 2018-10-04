using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {

        ClosestFriendLocationContext _context;
        public FriendRepository(ClosestFriendLocationContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Friend> GetComplete()
        {
            return _context.Set<Friend>()
                .Include("Address")
                .Include("Address.Location");
        }
    }
}
