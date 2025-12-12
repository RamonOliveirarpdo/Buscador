using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
