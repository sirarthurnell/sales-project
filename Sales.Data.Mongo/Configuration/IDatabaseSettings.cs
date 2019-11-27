namespace Sales.Data.Mongo.Configuration
{
    public interface IDatabaseSettings
    {
        string SalesCollectionName { get; set; }
        string CountriesCollectionName { get; set; }
        string ItemTypesCollectionName { get; set; }
        string OrderPrioritiesCollectionName { get; set; }
        string RegionsCountriesCollectionName { get; set; }
        string RegionsCollectionName { get; set; }
        string SalesChannelsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}