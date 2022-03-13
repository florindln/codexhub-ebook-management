using CodexhubCommon;
using InventoryService.Conversion;
using InventoryService.Dtos;
using InventoryService.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookUserController : ControllerBase
    {
        private IRepository<BookUserEntity> bookUserRepository;

        public BookUserController(IRepository<BookUserEntity> bookUserRepository)
        {
            this.bookUserRepository = bookUserRepository;
        }

        // get all books from a user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookUserDto>>> GetBooksByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var books = (await bookUserRepository.GetAllAsync(bookUser => bookUser.UserId == userId))
                            .Select(book => book.AsDto());
            return Ok(books);
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
