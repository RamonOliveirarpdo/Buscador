using Buscador.Dtos;

namespace Buscador.Interfaces
{
    public interface IUserAplication
    {
        Task<UserResponse> GetUserNameAsync(int userId);

        Task<UserResponse> CreateUserAsync(UserRequest user);
    }
}
