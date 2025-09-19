using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Models;       
using Services;      

namespace BookStoreManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookServices _bookService;

        public BooksController(BookServices bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all books")]
        public ActionResult<List<Books>> Get() => _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        [SwaggerOperation(Summary = "Get book by ID")]
        public ActionResult<Books> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost("add-one", Name = "AddOneBook")]
        [SwaggerOperation(Summary = "Add one book")]
        public ActionResult<Books> Create(Books book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPost("bulk-add", Name = "AddBooksInBulk")]
        [SwaggerOperation(Summary = "Add multiple books in bulk")]
        public ActionResult<List<Books>> CreateBulk(List<Books> books)
        {
            _bookService.CreateBulk(books);
            return Created("", books);
        }

        [HttpPut("{id:length(24)}", Name = "UpdateBookById")]
        [SwaggerOperation(Summary = "Update book by ID")]
        public IActionResult Update(string id, Books bookIn)
        {
            try
            {
                _bookService.Update(id, bookIn);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBookById")]
        [SwaggerOperation(Summary = "Delete book by ID")]
        public IActionResult Delete(string id)
        {
            try
            {
                _bookService.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
