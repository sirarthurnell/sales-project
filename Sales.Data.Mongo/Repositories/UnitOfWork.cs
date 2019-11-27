using System.Collections.Generic;
using Sales.Data.Entities;
using Sales.Data.Mongo.Configuration;
using Sales.Data.Mongo.Db;
using Sales.Data.Mongo.Mapper;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        static UnitOfWork()
        {
            var mappers = new List<IMongoMapper>
            {
                new GeneralConventionsMapper(),
                new SaleMapper(),
                new CountryMapper(),
                new ItemTypeMapper(),
                new OrderPriorityMapper(),
                new RegionMapper(),
                new SalesChannelMapper(),
                new RegionsCountriesMapper()
            };

            mappers.ForEach(m => m.Init());
        }

        private readonly IDatabaseSettings _settings;
        private readonly MongoDbContext _context;
        private readonly ISalesRepository _salesRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly IItemTypesRepository _itemTypesRepository;
        private readonly IOrderPrioritiesRepository _orderPrioritiesRepository;
        private readonly IRegionsRepository _regionsRepository;
        private readonly IRegionsCountriesRepository _regionsCountriesRepository;
        private readonly ISalesChannelsRepository _salesChannelsRepository;

        public UnitOfWork(IDatabaseSettings settings)
        {
            _settings = settings;
            _context = new MongoDbContext(_settings);
            _salesRepository = new SalesRepository(_context.Sales);
            _countriesRepository = new CountriesRepository(_context.Countries);
            _itemTypesRepository = new ItemTypesRepository(_context.ItemTypes);
            _orderPrioritiesRepository = new OrderPrioritiesRepository(_context.OrderPriorities);
            _regionsRepository = new RegionsRepository(_context.Regions);
            _regionsCountriesRepository = new RegionsCountriesRepository(_context.RegionsCountries);
            _salesChannelsRepository = new SalesChannelsRepository(_context.SalesChannels);
        }

        public ISalesRepository SalesRepository => _salesRepository;
        public ICountriesRepository CountriesRepository => _countriesRepository;
        public IItemTypesRepository ItemTypesRepository => _itemTypesRepository;
        public IOrderPrioritiesRepository OrderPrioritiesRepository => _orderPrioritiesRepository;
        public IRegionsCountriesRepository RegionsCountriesRepository => _regionsCountriesRepository;
        public IRegionsRepository RegionsRepository => _regionsRepository;
        public ISalesChannelsRepository SalesChannelsRepository => _salesChannelsRepository;

        public void Complete() { }

        public void Dispose() { }
    }
}