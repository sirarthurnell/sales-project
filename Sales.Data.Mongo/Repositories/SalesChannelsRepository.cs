using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class SalesChannelsRepository : ISalesChannelsRepository
    {
        private readonly IMongoCollection<SalesChannel> _salesChannelsCollection;

        public SalesChannelsRepository(IMongoCollection<SalesChannel> salesChannelsCollection)
        {
            _salesChannelsCollection = salesChannelsCollection;
        }

        public IEnumerable<SalesChannel> GetAll()
        {
            return _salesChannelsCollection
                .Find<SalesChannel>(namedItem => true)
                .ToList();
        }
    }
}