using Models;

namespace Services
{
    public interface IBookService
    {
        // GET all books
        List<Books> Get();

        // GET a single book by Id
        Books? Get(string id);

        // POST / Create a single book
        Books Create(Books book);

        // POST / Create multiple books (bulk insert)
        List<Books> CreateBulk(List<Books> books);

        // PUT / Update a book by Id (partial update support)
        void Update(string id, Books bookIn);

        // DELETE / Remove a book by Id
        void Remove(string id);
    }
}
