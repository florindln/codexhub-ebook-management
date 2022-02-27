﻿using Functional.Maybe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Conversion;
using UserService.Dtos;
using UserService.Models;
using UserService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await userRepository.GetAsync(id);

            if (user == null)
                return NotFound();

            return user.AsModel().AsDto();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post(UserDto userDto)
        {
            var user = userDto.AsModel();

            await userRepository.CreateAsync(user.AsData());

            return CreatedAtAction(nameof(GetById), user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(Guid id, UpdateUserDto updateUserDto)
        {
            var existing = await userRepository.GetAsync(id);

            if (existing == null)
                return NotFound();

            var user = new User(existing.Id, existing.FirstName, existing.LastName,
                updateUserDto.Email, updateUserDto.Interests.ToMaybe());

            await userRepository.UpdateAsync(user.AsData());

            return user.AsDto();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await userRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
