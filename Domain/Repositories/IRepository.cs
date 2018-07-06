using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> Get();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
