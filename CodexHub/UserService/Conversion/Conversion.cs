using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models;

namespace UserService.Conversion
{
    public static class Conversion
    {
        public static User AsModel(this UserDto userDto, string role) =>
            new User(userDto.Id, userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password, userDto.Interests.ToMaybe(), role);
        public static User AsModel(this UserEntity userEntity) =>
            new User(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email, userEntity.Password, userEntity.Interests.ToMaybe(), userEntity.Role);

        public static UserDto AsDto(this User user) =>
            new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Interests = user.Interests.OrElse(new List<string>()),
                Role = user.Role
            };

        public static UserEntity AsData(this User user) =>
            new UserEntity
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Interests = user.Interests.OrElse(new List<string>()),
                Role = user.Role,
            };

        public static void NotNull<T>(this T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
