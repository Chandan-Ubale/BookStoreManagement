using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class BookServices : IBookService
    {
        private readonly IMongoCollection<Books> _books;

        public BookServices(IOptions<BookstoreDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _books = database.GetCollection<Books>(settings.Value.BooksCollectionName);
        }

        // GET all books
        public List<Books> Get() => _books.Find(book => true).ToList();

        // GET a single book by Id
        public Books? Get(string id) =>
            _books.Find(book => book.Id == id).FirstOrDefault();

        // POST / Create a single book
        public Books Create(Books book)
        {
            _books.InsertOne(book);
            return book;
        }

        // POST / Create multiple books (bulk insert)
        public List<Books> CreateBulk(List<Books> books)
        {
            _books.InsertMany(books);
            return books;
        }

        // PUT / Update a book by Id (partial update support)
        public void Update(string id, Books bookIn)
        {
            var updates = new List<UpdateDefinition<Books>>();
            var updateBuilder = Builders<Books>.Update;

            if (!string.IsNullOrEmpty(bookIn.Title))
                updates.Add(updateBuilder.Set(b => b.Title, bookIn.Title));

            if (!string.IsNullOrEmpty(bookIn.Author))
                updates.Add(updateBuilder.Set(b => b.Author, bookIn.Author));

            if (bookIn.Price > 0)
                updates.Add(updateBuilder.Set(b => b.Price, bookIn.Price));

            if (updates.Count == 0)
                throw new ArgumentException("No valid fields provided to update.");

            var updateDefinition = updateBuilder.Combine(updates);

            var result = _books.UpdateOne(book => book.Id == id, updateDefinition);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException($"Book with Id '{id}' not found.");
        }

        // DELETE / Remove a book by Id
        public void Remove(string id)
        {
            var result = _books.DeleteOne(book => book.Id == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"Book with Id '{id}' not found.");
        }
    }
}
