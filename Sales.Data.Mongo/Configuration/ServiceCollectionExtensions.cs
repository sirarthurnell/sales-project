using System;
using Microsoft.Extensions.DependencyInjection;
using Sales.Data.Mongo.Repositories;
using Sales.Data.Repositories;

namespace Sales.Data.Mongo.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDb(this IServiceCollection services, Action<DatabaseSettings> configuration)
        {
            var dbSettings = new DatabaseSettings();
            configuration(dbSettings);
            services.AddSingleton<IDatabaseSettings>(dbSettings);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}