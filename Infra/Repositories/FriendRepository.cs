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

        TesteViaVarejoContext _context;
        public FriendRepository(TesteViaVarejoContext context) : base(context)
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
