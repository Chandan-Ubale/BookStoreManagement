using BookStoreManagement.Core.Interfaces;
using BookStoreManagement.Core.Models;
using BookStoreManagement.Infra.Data;
using MongoDB.Driver;

namespace BookStoreManagement.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Books> _books;

        public BookRepository(MongoDbContext context)
        {
            _books = context.Books;
        }

        public List<Books> GetAllBooks() =>
            _books.Find(book => true).ToList();

        public Books? GetBookById(string id) =>
            _books.Find(book => book.Id == id).FirstOrDefault();

        public void AddBook(Books book) =>
            _books.InsertOne(book);

        public void AddBooksBulk(List<Books> books) =>
            _books.InsertMany(books);

        public void UpdateBook(string id, Books bookIn)
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

        public void DeleteBook(string id)
        {
            var result = _books.DeleteOne(book => book.Id == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"Book with Id '{id}' not found.");
        }
    }
}
