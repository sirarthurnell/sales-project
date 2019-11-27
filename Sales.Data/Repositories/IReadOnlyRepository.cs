using System.Collections.Generic;
using Sales.Data.Entities;

namespace Sales.Data.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
