using ClosestFriendLocation.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ClosestFriendLocation.Domain.Interfaces.Services
{
    public interface IService<T> where T : BaseEntity
    {
        List<T> List();
        T Find(Guid id);
        void Delete(T obj);
        void Add(T obj);
        void Update(T obj);
    }
}
