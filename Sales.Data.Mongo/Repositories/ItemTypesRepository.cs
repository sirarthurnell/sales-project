using System.Collections.Generic;
using MongoDB.Driver;
using Sales.Data.Entities;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Repositories
{
    public class ItemTypesRepository : IItemTypesRepository
    {
        private readonly IMongoCollection<ItemType> _itemTypesCollection;

        public ItemTypesRepository(IMongoCollection<ItemType> itemTypesCollection)
        {
            _itemTypesCollection = itemTypesCollection;
        }

        public IEnumerable<ItemType> GetAll()
        {
            return _itemTypesCollection
                .Find<ItemType>(namedItem => true)
                .ToList();
        }
    }
}