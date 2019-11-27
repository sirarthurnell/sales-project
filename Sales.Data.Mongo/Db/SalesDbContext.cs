using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Mongo.Configuration;

namespace Sales.Data.Mongo.Db
{
    class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSettings _settings;

        public MongoDbContext(IDatabaseSettings settings)
        {
            _settings = settings;

            var client = new MongoClient(_settings.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Sale> Sales
        {
            get
            {
                return _database.GetCollection<Sale>(_settings.SalesCollectionName);
            }
        }

        public IMongoCollection<Country> Countries
        {
            get
            {
                return _database.GetCollection<Country>(_settings.CountriesCollectionName);
            }
        }

        public IMongoCollection<ItemType> ItemTypes
        {
            get
            {
                return _database.GetCollection<ItemType>(_settings.ItemTypesCollectionName);
            }
        }

        public IMongoCollection<OrderPriority> OrderPriorities
        {
            get
            {
                return _database.GetCollection<OrderPriority>(_settings.OrderPrioritiesCollectionName);
            }
        }

        public IMongoCollection<Region> Regions
        {
            get
            {
                return _database.GetCollection<Region>(_settings.RegionsCollectionName);
            }
        }

        public IMongoCollection<RegionCountry> RegionsCountries
        {
            get
            {
                return _database.GetCollection<RegionCountry>(_settings.RegionsCountriesCollectionName);
            }
        }

        public IMongoCollection<SalesChannel> SalesChannels
        {
            get
            {
                return _database.GetCollection<SalesChannel>(_settings.SalesChannelsCollectionName);
            }
        }
    }
}