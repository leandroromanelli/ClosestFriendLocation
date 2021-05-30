using ClosestFriendLocation.Domain.Entities;
using System.Collections.Generic;

namespace ClosestFriendLocation.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> Get();

        void Add(T obj);

        void Update(T obj);

        void Delete(T obj);
    }
}
