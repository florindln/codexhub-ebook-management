using CodexhubCommon;
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
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> repository;

        public RatingController(IRepository<Rating> repository)
        {
            this.repository = repository;
        }

        // GET api/<RatingController>/5
        [HttpGet("{bookId}")]
        public async Task<object> Get(Guid bookId)
        {
            var ratings = await repository.GetAllAsync(rating => rating.BookId == bookId);

            if (ratings.Count == 0)
                return new
                {
                    AverageRating = 0,
                    RatingsCount = 0
                };

            var averageRating = ratings.Select(rating => rating.StarRating).Average();

            return new
            {
                AverageRating = averageRating,
                RatingsCount = ratings.Count
            };
        }

        [HttpGet("{bookId}/user/{userId}")]
        public async Task<double> GetUserRating(Guid bookId, Guid userId)
        {
            var rating = await repository.GetAsync(rating => rating.UserId == userId && rating.BookId == bookId);

            if (rating == null)
                return 0f;

            return rating.StarRating;
        }

        // POST api/<RatingController>
        [HttpPost]
        public async Task<IActionResult> Post(RatingDto ratingDto)
        {
            if (ratingDto.Rating < 1 || ratingDto.Rating > 5)
                return BadRequest("Rating must be betwen 1 and 5 stars");

            var existingRating = await repository.GetAsync(rating => rating.BookId == ratingDto.BookId && rating.UserId == ratingDto.UserId);

            if (existingRating != null)
            {
                var newRating = new Rating
                {
                    Id = existingRating.Id,
                    BookId = existingRating.BookId,
                    UserId = existingRating.UserId,
                    StarRating = ratingDto.Rating,
                };

                await repository.UpdateAsync(newRating);
                return NoContent();
            }

            Rating rating = new Rating
            {
                Id = Guid.NewGuid(),
                BookId = ratingDto.BookId,
                UserId = ratingDto.UserId,
                StarRating = ratingDto.Rating,
            };
            await repository.CreateAsync(rating);
            return Ok();
        }
    }
}
