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
        public static User AsModel(this UserDto userDto) =>
            new User(userDto.Id, userDto.FirstName, userDto.LastName, userDto.Email, userDto.Interests.ToMaybe());
        public static User AsModel(this UserEntity userEntity) =>
            new User(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email, userEntity.Interests.ToMaybe());

        public static UserDto AsDto(this User user) =>
            new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Interests = user.Interests.OrElse(new List<string>()),
            };

        public static UserEntity AsData(this User user) =>
            new UserEntity
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Interests = user.Interests.OrElse(new List<string>()),
            };

        public static void NotNull<T>(this T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
