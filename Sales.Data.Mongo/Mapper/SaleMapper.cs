using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Sales.Data.Entities;

namespace Sales.Data.Mongo.Mapper
{
    class SaleMapper : IMongoMapper
    {
        public void Init()
        {
            BsonClassMap.RegisterClassMap<Sale>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(sale => sale.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}