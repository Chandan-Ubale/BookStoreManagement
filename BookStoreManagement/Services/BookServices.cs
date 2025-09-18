using BookStoreManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BookStoreManagement.Services
{
    public class BookServices
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
        public Books Get(string id) => _books.Find(book => book.Id == id).FirstOrDefault();

        // POST / Create a single book
        public Books Create(Books book)
        {
            _books.InsertOne(book); // MongoDB auto-generates Id
            return book;
        }

        // POST / Create multiple books (bulk insert)

        public List<Books> CreateBulk(List<Books> books)
        {
            _books.InsertMany(books); // MongoDB auto-generates Ids for each book
            return books;
        }

        // PUT / Update a book by Id
        public void Update(string id, Books bookIn)
        {
            bookIn.Id = id; // ensure _id remains unchanged
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        // DELETE / Remove a book by Id
        public void Remove(string id)
        {
            var result = _books.DeleteOne(book => book.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"Book with Id '{id}' not found.");
            }
        }
    }
}
