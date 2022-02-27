using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, string email, Maybe<List<string>> interests)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Interests = interests;
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; set; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public Maybe<List<string>> Interests { get; }

        public DateTime CreatedAt { get; }

    }
}
