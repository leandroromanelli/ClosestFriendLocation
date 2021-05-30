using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Interfaces.Services;
using ClosestFriendLocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestFriendLocation.Domain.Services
{
    public abstract class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public List<T> List()
        {
            return _repository.Get().ToList();
        }

        public T Find(Guid id)
        {
            return _repository.Get().FirstOrDefault(x => x.Id == id);
        }

        public void Delete(T obj)
        {
            _repository.Delete(obj);
        }

        public void Add(T obj)
        {
            _repository.Add(obj);
        }

        public void Update(T obj)
        {
            _repository.Update(obj);
        }
    }
}
