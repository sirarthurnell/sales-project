using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class SalesRepository : ISalesRepository 
    {
        private readonly IMongoCollection<Sale> _salesCollection;

        public SalesRepository(IMongoCollection<Sale> salesCollection)
        {
            _salesCollection = salesCollection;
        }

        public long CountTotal(Expression<Func<Sale, bool>> filter = null)
        {
            return _salesCollection.CountDocuments(filter);
        }

        public Sale Create(Sale sale)
        {
            _salesCollection.InsertOne(sale);
            return sale;
        }

        public void Delete(string id)
        {
            _salesCollection.DeleteOne(savedSale => savedSale.Id == id);
        }

        public IEnumerable<Sale> Get(Expression<Func<Sale, bool>> filter = null, Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderBy = null, int skip = 0, int take = int.MaxValue)
        {
            IQueryable<Sale> query = _salesCollection.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            IQueryable<Sale> skippedAndTaken = query;
            IOrderedQueryable<Sale> ordered;
            if (orderBy != null)
            {
                ordered = orderBy(query);
                skippedAndTaken = (IQueryable<Sale>)ordered;
            }

            if (skip != 0)
            {
                skippedAndTaken = skippedAndTaken.Skip(skip);
            }

            if (take != int.MaxValue)
            {
                skippedAndTaken = skippedAndTaken.Take(take);
            }

            return skippedAndTaken.ToList();
        }

        public Sale GetById(string id)
        {
            return _salesCollection
                .Find(sale => sale.Id == id)
                .FirstOrDefault();
        }

        public void Update(Sale sale)
        {
            _salesCollection.ReplaceOne(savedSale => savedSale.Id == sale.Id, sale);
        }
    }
}