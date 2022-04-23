using CodexhubCommon;
using Functional.Maybe;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Conversion;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models;
using static Contracts.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    //[Authorize]
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<UserEntity> userRepository;
        private readonly IPublishEndpoint publishEndpoint;


        public UsersController(IRepository<UserEntity> userRepository, IPublishEndpoint publishEndpoint)
        {
            this.userRepository = userRepository;
            this.publishEndpoint = publishEndpoint;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = (await userRepository.GetAllAsync()).Select(x => x.AsModel().AsDto());
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await userRepository.GetAsync(id);

            if (user == null)
                return NotFound();

            return user.AsModel().AsDto();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(UserDto userDto)
        {
            if (userDto.Id != Guid.Empty)
                throw new ArgumentException("Cannot provide id when creating an user");

            var existingUser = await userRepository.GetAsync(user => user.Email == userDto.Email);

            if (existingUser != null)
                return Conflict(new { message = "User with the same email already exists" });

            userDto.Id = Guid.NewGuid();

            var user = userDto.AsModel();

            await userRepository.CreateAsync(user.AsData());


            await publishEndpoint.Publish(new CatalogUserCreated(user.Id, user.Email, user.Interests.OrElse(new List<string>())));

            return Created(nameof(GetByIdAsync), user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var existing = await userRepository.GetAsync(id);

            if (existing == null)
                return NotFound();

            var user = new User(existing.Id, updateUserDto.FirstName, updateUserDto.LastName,
                updateUserDto.Email, existing.Password, updateUserDto.Interests.ToMaybe(), existing.Role);

            await userRepository.UpdateAsync(user.AsData());

            await publishEndpoint.Publish(new CatalogUserUpdated(user.Id, user.Interests.OrElse(new List<string>())));

            return user.AsDto();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await userRepository.DeleteAsync(id);

            await publishEndpoint.Publish(new CatalogUserDeleted(id));

            return NoContent();
        }
    }
}
