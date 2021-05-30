using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Repositories;
using ClosestFriendLocation.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ClosestFriendLocation.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        ClosestFriendLocationContext _context;

        public Repository(ClosestFriendLocationContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
