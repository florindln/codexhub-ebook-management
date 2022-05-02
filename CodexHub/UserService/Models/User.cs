using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, string email, string password, Maybe<List<string>> interests, string role)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(email));
            Interests = interests;
            CreatedAt = DateTime.Now;
            Role = role ?? throw new ArgumentNullException(nameof(role));
        }
        public Guid Id { get; set; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Password { get; }
        public string Role { get; }

        public Maybe<List<string>> Interests { get; }

        public DateTime CreatedAt { get; }

    }
}
