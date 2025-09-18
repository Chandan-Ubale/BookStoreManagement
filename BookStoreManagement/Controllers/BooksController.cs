using BookStoreManagement.Models;
using BookStoreManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        // GET: api/books
        [HttpGet]
        public ActionResult<List<Books>> Get() => _bookService.Get();

        // GET: api/books/{id}
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Books> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();
            return book;
        }

        // POST: api/books (single insert)
        [HttpPost]
        public ActionResult<Books> Create(Books book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        // POST: api/books/bulk (bulk insert)
        [HttpPost("bulk")]
        public ActionResult<List<Books>> CreateBulk(List<Books> books)
        {
            _bookService.CreateBulk(books);
            return Created("", books);
        }

        // PUT: api/books/{id}
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Books bookIn)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();

            _bookService.Update(id, bookIn);
            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();

            _bookService.Remove(id);
            return NoContent();
        }
    }
}
