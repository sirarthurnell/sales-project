using System;
using AspNetCore.Identity.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Sales.Authentication.Mongo.IdentityModels;

namespace Sales.Authentication.Mongo.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoIdentity(this IServiceCollection services, Action<AuthenticationDatabaseSettings> configuration)
        {
            var dbSettings = new AuthenticationDatabaseSettings();
            configuration(dbSettings);
            services.AddIdentityMongoDbProvider<ApplicationUser>(identity =>
            {
                identity.Password.RequiredLength = 6;
                identity.Password.RequireLowercase = true;
                identity.Password.RequireUppercase = true;
                identity.Password.RequireNonAlphanumeric = true;
                identity.Password.RequireDigit = true;
            },
            mongo =>
            {
                mongo.ConnectionString = dbSettings.ConnectionString;
            });
        }
    }
}