using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sales.Data.Entities;

namespace Sales.Data.Repositories
{
    public interface ISalesRepository
    {
        long CountTotal(Expression<Func<Sale, bool>> filter = null);

        IEnumerable<Sale> Get(
           Expression<Func<Sale, bool>> filter = null,
           Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderBy = null,
           int skip = 0,
           int take = int.MaxValue
        );

        Sale GetById(string id);

        Sale Create(Sale sale);

        void Update(Sale sale);

        void Delete(string id);
    }
}