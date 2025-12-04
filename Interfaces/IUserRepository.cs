using Buscador.Dtos.Requests;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> LoginAsync(LoginRequest loginRequest);
        Task<User> AddUserAsync(User user);
        Task<int> SaveChangesAsync();
    }
}
