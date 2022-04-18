using CodexhubCommon;
using Functional.Maybe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Auth;
using UserService.Conversion;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<UserEntity> userRepository;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(IRepository<UserEntity> userRepository, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.userRepository = userRepository;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (userDto.Id != Guid.Empty)
                throw new ArgumentException("Cannot provide id when creating an user");

            var existingUser = await userRepository.GetAsync(user => user.Email == userDto.Email);

            if (existingUser != null)
                return Conflict(new { message = "User with the same email already exists" });

            userDto.Id = Guid.NewGuid();
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = userDto.AsModel();

            await userRepository.CreateAsync(user.AsData());

            return Created("register", user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userRepository.GetAsync(user => user.Email == loginDto.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid credentials" });

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return BadRequest(new { message = "Invalid credentials" });

            var token = jwtAuthenticationManager.Authenticate(user);

            if (token is null)
                return Unauthorized();

            return Ok(new { token = token });
        }
    }
}