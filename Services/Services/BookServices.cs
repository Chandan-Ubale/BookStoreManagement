using BookStoreManagement.Core.Interfaces;
using BookStoreManagement.Core.Models;

namespace BookStoreManagement.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Books> GetAllBooks() => _repository.GetAllBooks();
        public Books? GetBookById(string id) => _repository.GetBookById(id);
        public void AddBook(Books book) => _repository.AddBook(book);
        public void AddBooksBulk(List<Books> books) => _repository.AddBooksBulk(books);
        public void UpdateBook(string id, Books bookIn) => _repository.UpdateBook(id, bookIn);
        public void DeleteBook(string id) => _repository.DeleteBook(id);
    }
}
