using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class RegionsCountriesRepository : IRegionsCountriesRepository
    {
        private readonly IMongoCollection<RegionCountry> _regionsCountriesCollection;

        public RegionsCountriesRepository(IMongoCollection<RegionCountry> regionsCountriesCollection)
        {
            _regionsCountriesCollection = regionsCountriesCollection;
        }

        public IEnumerable<RegionCountry> GetAll()
        {
            return _regionsCountriesCollection
                .Find<RegionCountry>(regionCountry => true)
                .ToList();
        }
    }
}