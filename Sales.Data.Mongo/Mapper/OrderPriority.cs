using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Sales.Data.Entities;

namespace Sales.Data.Mongo.Mapper
{
    public class OrderPriorityMapper: IMongoMapper
    {
        public void Init()
        {
            BsonClassMap.RegisterClassMap<OrderPriority>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(item => item.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}