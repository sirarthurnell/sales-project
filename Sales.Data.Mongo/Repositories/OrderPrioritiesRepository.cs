using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class OrderPrioritiesRepository : IOrderPrioritiesRepository
    {
        private readonly IMongoCollection<OrderPriority> _orderPrioritiesCollection;

        public OrderPrioritiesRepository(IMongoCollection<OrderPriority> orderPrioritiesCollection)
        {
            _orderPrioritiesCollection = orderPrioritiesCollection;
        }

        public IEnumerable<OrderPriority> GetAll()
        {
            return _orderPrioritiesCollection
                .Find<OrderPriority>(namedItem => true)
                .ToList();
        }
    }
}