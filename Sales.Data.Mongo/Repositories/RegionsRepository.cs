using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly IMongoCollection<Region> _regionsCollection;

        public RegionsRepository(IMongoCollection<Region> regionsCollection)
        {
            _regionsCollection = regionsCollection;
        }

        public IEnumerable<Region> GetAll()
        {
            return _regionsCollection
                .Find<Region>(namedItem => true)
                .ToList();
        }
    }
}