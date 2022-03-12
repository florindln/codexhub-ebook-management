using System;

namespace UserService.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}