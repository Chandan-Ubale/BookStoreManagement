using BookStoreManagement.Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreManagement.Infra.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<BookstoreDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Books> Books =>
            _database.GetCollection<Books>("Books");
    }
}
