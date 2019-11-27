namespace Sales.Data.Mongo.Configuration
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string SalesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CountriesCollectionName { get; set; }
        public string ItemTypesCollectionName { get; set; }
        public string OrderPrioritiesCollectionName { get; set; }
        public string RegionsCountriesCollectionName { get; set; }
        public string RegionsCollectionName { get; set; }
        public string SalesChannelsCollectionName { get; set; }
    }
}