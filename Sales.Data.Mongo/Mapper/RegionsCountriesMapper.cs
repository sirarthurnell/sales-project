using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Sales.Data.Entities;

namespace Sales.Data.Mongo.Mapper
{
    public class RegionsCountriesMapper : IMongoMapper
    {
        public void Init()
        {
            BsonClassMap.RegisterClassMap<RegionCountry>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(rc => rc.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}