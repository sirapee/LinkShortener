using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using LinkShortener.Data;
using LinkShortener.Entities;
using LinkShortener.Repositories;
using LinkShortener.RepositoryImplemtation;
using LinkShortener.ServiceImplementation;
using LinkShortener.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace LinkShortener.Configurations
{
    public static class StartupExtensions
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<IShortenerService, ShortenerService>();
        }
        
        public static void SetupSqlServerDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase(configuration.GetConnectionString("DatabaseName")
                 , builder =>
                 {
                     
                 }));
            services.AddMemoryCache();

            //AddIndexes(configuration);

       
        }

    }
}