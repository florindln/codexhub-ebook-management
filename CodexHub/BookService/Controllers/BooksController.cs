using BookService.Dtos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Contracts.Contracts;
using static Contracts.Secrets;
using BookService.Conversions;
using BookService.Data;
using BookService.AppServices;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly IPublishEndpoint publishEndpoint;
        private readonly BookApp bookApp;
        private readonly ILogger logger;

        private const string BOOK_PROVIDER_BASE_URL = "https://www.googleapis.com/books/v1";

        public BooksController(IPublishEndpoint publishEndpoint, BookApp bookApp, ILogger logger)
        {
            this.publishEndpoint = publishEndpoint;
            this.bookApp = bookApp;
            this.logger = logger;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return await bookApp.GetAllBooks();
        }

        //[HttpGet("Filter")]
        //public async Task<IEnumerable<BookDto>> GetAllAsync(string pageCountMin, string pageCountMax, string publishedYearMin, string publishedYearMax, [FromQuery(Name = "genres[]")] string[] genres)
        //{
        //    var books = await bookApp.GetAllBooks();
        //    var filtered = books.Where(book => pageCountMin == null || Convert.ToInt32(pageCountMin) < book.PageCount)
        //    .Where(book => pageCountMax == null || Convert.ToInt32(pageCountMax) > book.PageCount)
        //    .Where(book => publishedYearMin == null || Convert.ToInt32(publishedYearMin) < book.PublishedDate.Year)
        //    .Where(book => publishedYearMax == null || Convert.ToInt32(publishedYearMax) > book.PublishedDate.Year)
        //    .Where(book => genres.Count() == 0 || genres.Contains(book.Category));

        //    return filtered;
        //}

        [HttpGet("Filter")]
        public async Task<IEnumerable<BookDto>> GetAllAsync(string title, string pageCountMin, string pageCountMax, string publishedYearMin, string publishedYearMax, [FromQuery(Name = "genres[]")] string[] genres)
        {
            var books = await bookApp.GetAllBooks(book =>
                (pageCountMin == null || Convert.ToInt32(pageCountMin) < book.PageCount) &&
                (pageCountMax == null || Convert.ToInt32(pageCountMax) > book.PageCount) &&
                (publishedYearMin == null || Convert.ToInt32(publishedYearMin) < book.PublishedDate.Year) &&
                (publishedYearMax == null || Convert.ToInt32(publishedYearMax) > book.PublishedDate.Year) &&
                (genres.Count() == 0 || genres.Contains(book.Category)) &&
                (title == null || book.Title.Contains(title) || title.Contains(book.Title))
                );

            return books;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetByIdAsync(Guid id)
        {
            var book = await bookApp.GetBook(id);

            if (book == null)
                return NotFound();

            return book.AsDto();
        }

        [HttpGet("PullBooks")]
        public async Task GetBooksFromAPI(string name)
        {
            var res = await httpClient.GetAsync(
                BOOK_PROVIDER_BASE_URL + "/volumes?q=" + name + $"&key={GOOGLE_API_KEY}");

            var content = await res.Content.ReadAsStringAsync();
            var books = Conversion.GoogleAPIContentToBooks(content);

            await bookApp.CreateBooks(books);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDto bookDto)
        {
            var bookEntity = bookDto.AsEntity();
            await bookApp.CreateBook(bookEntity);

            try
            {
                await publishEndpoint.Publish(new CatalogBookCreated(bookEntity.Id, bookEntity.Title, bookEntity.InitialPrice));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), bookEntity);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBookDto updateBookDto)
        {
            var updatedBook = await bookApp.UpdateBook(id, updateBookDto);

            if (updatedBook == null)
                return NotFound();

            try
            {
                await publishEndpoint.Publish(new CatalogBookUpdated(id, updatedBook.InitialPrice));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await bookApp.DeleteBook(id);

            try
            {
                await publishEndpoint.Publish(new CatalogBookDeleted(id));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }


            return NoContent();
        }
    }
}
