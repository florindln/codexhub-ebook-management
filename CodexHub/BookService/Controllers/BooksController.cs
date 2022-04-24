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
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

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
        private readonly IAmazonS3 amazons3;

        private const string BOOK_PROVIDER_BASE_URL = "https://www.googleapis.com/books/v1";

        public BooksController(IPublishEndpoint publishEndpoint, BookApp bookApp, ILogger logger, IAmazonS3 amazons3)
        {
            this.publishEndpoint = publishEndpoint;
            this.bookApp = bookApp;
            this.logger = logger;
            this.amazons3 = amazons3;
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadBook(Guid id)
        {
            var book = await bookApp.GetBook(id);
            if (book == null)
                return NotFound();


            //using (AmazonEC2Client ec2Client = new AmazonEC2Client(RegionEndpoint.USWest2))


            var request = new GetObjectRequest
            {
                BucketName = "codexhub",
                Key = book.Title.Replace(":", "") + ".pdf"
            };

            try
            {
                using GetObjectResponse response = await amazons3.GetObjectAsync(request);

                using Stream responseStream = response.ResponseStream;
                var stream = new MemoryStream();
                await responseStream.CopyToAsync(stream);
                stream.Position = 0;

                return new FileStreamResult(stream, response.Headers["Content-Type"])
                {
                    FileDownloadName = book.Title + ".pdf"

                };
            }
            catch (AmazonS3Exception ex)
            {
                return NotFound(ex.Message);
            }

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

            foreach (var book in books)
            {
                await publishEndpoint.Publish(new CatalogBookCreated(book.Id, book.Title, book.Description, book.InitialPrice, book.Category));
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDto bookDto)
        {
            var bookEntity = bookDto.AsEntity();
            await bookApp.CreateBook(bookEntity);

            //try
            //{
            await publishEndpoint.Publish(new CatalogBookCreated(bookEntity.Id, bookEntity.Title, bookEntity.Description, bookEntity.InitialPrice, bookEntity.Category));
            //}
            //catch (Exception e)
            //{
            //    logger.LogError(e.Message);
            //}

            return Created(nameof(GetByIdAsync), bookEntity);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBookDto updateBookDto)
        {
            var updatedBook = await bookApp.UpdateBook(id, updateBookDto);

            if (updatedBook == null)
                return NotFound();

            //try
            //{
            await publishEndpoint.Publish(new CatalogBookUpdated(id, updatedBook.Description, updatedBook.InitialPrice));
            //}
            //catch (Exception e)
            //{
            //    logger.LogError(e.Message);
            //}

            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await bookApp.DeleteBook(id);

            //try
            //{
            await publishEndpoint.Publish(new CatalogBookDeleted(id));
            //}
            //catch (Exception e)
            //{
            //    logger.LogError(e.Message);
            //}


            return NoContent();
        }
    }
}
