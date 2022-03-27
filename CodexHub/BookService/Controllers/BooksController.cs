using BookService.Dtos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<BookDto> books = new List<BookDto>()
        {
            new BookDto(Guid.Parse("65abc189-c324-0b8f-01bb-f0a6dde6e1d5"),"testtile","testauthor","testdescription",5.0 ),
            new BookDto(Guid.NewGuid(),"testtile","testauthor","testdescription",5.0 ),
        };

        private readonly IPublishEndpoint publishEndpoint;

        public BooksController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<BookDto> Get()
        {
            return books;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetById(Guid id)
        {
            var book = books.Where(x => x.Id == id).SingleOrDefault();

            if (book == null)
                return NotFound();

            return book;
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDto book)
        {
            var newbook = new BookDto(Guid.NewGuid(), book.Title, book.Author, book.Description, book.InitialPrice);
            books.Add(newbook);

            await publishEndpoint.Publish(new CatalogBookCreated(newbook.Id, newbook.Title, newbook.InitialPrice));

            return CreatedAtAction(nameof(GetById), new { id = newbook.Id }, newbook);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBookDto updateBookDto)
        {
            var existingBook = books.Where(x => x.Id == id).SingleOrDefault();

            if (existingBook == null)
                return NotFound();

            var updatedBook = existingBook with
            {
                InitialPrice = updateBookDto.InitialPrice,
            };

            var index = books.FindIndex(x => x.Id == id);
            books[index] = updatedBook;

            await publishEndpoint.Publish(new CatalogBookUpdated(id, updatedBook.InitialPrice));


            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var index = books.FindIndex(x => x.Id == id);

            if (index < 0)
                return NotFound();

            books.RemoveAt(index);

            await publishEndpoint.Publish(new CatalogBookDeleted(id));

            return NoContent();
        }
    }
}
