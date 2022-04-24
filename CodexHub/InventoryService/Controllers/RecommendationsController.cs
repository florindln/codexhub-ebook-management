using CodexhubCommon;
using InventoryService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRepository<CatalogBook> catalogBookRepository;
        private readonly IRepository<CatalogUser> catalogUserRepository;

        public RecommendationsController(IRepository<CatalogBook> catalogBookRepository, IRepository<CatalogUser> catalogUserRepository)
        {
            this.catalogBookRepository = catalogBookRepository;
            this.catalogUserRepository = catalogUserRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CatalogBook>> Get(int amount)
        {
            var books = await catalogBookRepository.GetAllAsync();

            var r = new Random();
            var res = books.OrderBy(x => r.Next()).Take(amount);

            return res;
        }

        // simple recommendation algorithm based on user interests and book categories
        [HttpGet("{userId}")]
        public async Task<IEnumerable<CatalogBook>> GetRecommendationByUserId(Guid userId, int amount)
        {
            var user = await catalogUserRepository.GetAsync(userId);
            var userInterests = user.Interests.Select(interest => interest.ToLower());

            var books = await catalogBookRepository.GetAllAsync();
            var bookCategories = books.Select(book => book.Category.ToLower()).Distinct();

            // intersect user interests and book categories
            var relevantCategories = new List<string>();
            foreach (var interest in userInterests)
            {
                foreach (var category in bookCategories)
                {
                    if (category.Contains(interest))
                        relevantCategories.Add(category);
                }
            }

            // get the books that contain the relevant categories
            var relevantBooks = books.Where(book => relevantCategories.Contains(book.Category.ToLower()));

            // shuffle the book list and take the amount requested
            var r = new Random();
            var res = relevantBooks.OrderBy(x => r.Next()).Take(amount);

            return res;
        }
    }
}
