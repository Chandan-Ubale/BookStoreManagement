using BookStoreManagement.Core.Models;

namespace BookStoreManagement.Core.Interfaces
{
    public interface IBookRepository
    {
        List<Books> GetAllBooks();
        Books? GetBookById(string id);
        void AddBook(Books book);
        void AddBooksBulk(List<Books> books);
        void UpdateBook(string id, Books bookIn);
        void DeleteBook(string id);
    }
}
