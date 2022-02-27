using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(UserEntity user);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<UserEntity>> GetAllAsync();
        Task<UserEntity> GetAsync(Guid id);
        Task UpdateAsync(UserEntity user);
    }
}