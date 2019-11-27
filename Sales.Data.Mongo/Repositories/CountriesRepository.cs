using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly IMongoCollection<Country> _countriesCollection;

        public CountriesRepository(IMongoCollection<Country> countriesCollection)
        {
            _countriesCollection = countriesCollection;
        }

        public IEnumerable<Country> GetAll()
        {
            return _countriesCollection
                .Find<Country>(namedItem => true)
                .ToList();
        }
    }
}