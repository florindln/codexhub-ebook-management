using CodexhubCommon;
using InventoryService.Clients;
using InventoryService.Conversion;
using InventoryService.Dtos;
using InventoryService.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookUserController : ControllerBase
    {
        private readonly IRepository<BookUserEntity> bookUserRepository;
        //private readonly BookCatalogClient bookCatalogClient;
        private readonly IRepository<CatalogBook> catalogBookRepository;

        public BookUserController(IRepository<BookUserEntity> bookUserRepository, IRepository<CatalogBook> catalogBookRepository, BookCatalogClient bookCatalogClient = null)
        {
            this.bookUserRepository = bookUserRepository;
            this.catalogBookRepository = catalogBookRepository;
            //this.bookCatalogClient = bookCatalogClient;
        }

        // get all books from a user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookUserDto>>> GetBooksByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var booksUsersEntities = (await bookUserRepository.GetAllAsync(bookUser => bookUser.UserId == userId));
            //var catalogBooks = await bookCatalogClient.GetCatalogBooksAsync();
            var bookIds = booksUsersEntities.Select(bookUser => bookUser.BookId);
            var catalogBooks = await catalogBookRepository.GetAllAsync(book => bookIds.Contains(book.Id));

            var bookUserDtos = booksUsersEntities.Select(booksUsersEntity =>
            {
                var catalogBook = catalogBooks.Single(catalogBook => catalogBook.Id == booksUsersEntity.BookId);
                return booksUsersEntity.AsDto(catalogBook.Title, catalogBook.InitialPrice);
            });

            return Ok(bookUserDtos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantBookDto grantBookDto)
        {
            var inventoryItem = await bookUserRepository.GetAsync(bookUser => bookUser.UserId == grantBookDto.UserId
                && bookUser.BookId == grantBookDto.BookId);
            if (inventoryItem == null)
            {
                inventoryItem = new BookUserEntity
                {
                    BookId = grantBookDto.BookId,
                    UserId = grantBookDto.UserId,
                    Id = Guid.NewGuid(),
                    BoughtAt = DateTime.Now,
                };

                await bookUserRepository.CreateAsync(inventoryItem);
                return Ok(inventoryItem);
            }
            else
                return BadRequest("User already owns the book");


        }


    }
}
