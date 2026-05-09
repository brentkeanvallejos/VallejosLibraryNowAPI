using VallejosLibraryNowAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace VallejosLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Harry Potter",
                Author = "J.K Rowling",
                Genre = "Fantasy",
                Available = true,
                PublishedYear =1997,
            },
            new Book
            {
                Id = 2,
                Title = "Legend of the Guardians: The Owls of Ga’Hoole",
                Author = "Kathryn Lasky",
                Genre = "Fantasy Fiction",
                Available = true,
                PublishedYear =2003,
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved"
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            }
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Books Retrieved"
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            // assign a new Id based on current max to avoid duplicates after deletions
            newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetId),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book Created"
                });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated"
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book deleted"
            });
        }
    }
}