using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext:ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //Categories = database.GetCollection<Category>(configuration.GetValue<string>("DatabaseSettings:CategoryCollection"));
            Categories = database.GetCollection<Category>("Categories");

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Category> Categories { get; }
    }
}
