using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(UserRequest User);
        Task<User> AddUserAsync(User user);
        Task<int> SaveChangesAsync();

    }
}
