using System;

namespace Sales.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICountriesRepository CountriesRepository { get; }
        IItemTypesRepository ItemTypesRepository { get; }
        IOrderPrioritiesRepository OrderPrioritiesRepository { get; }
        IRegionsCountriesRepository RegionsCountriesRepository { get; }
        IRegionsRepository RegionsRepository { get; }
        ISalesChannelsRepository SalesChannelsRepository { get; }
        ISalesRepository SalesRepository { get; }
        void Complete();
    }
}