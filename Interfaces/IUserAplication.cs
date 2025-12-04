using Buscador.Dtos;
using Buscador.Dtos.Requests;

namespace Buscador.Interfaces
{
    public interface IUserAplication
    {
        Task<UserResponse> GetUserNameAsync(int userId);

        Task<bool> LoginAsync(LoginRequest loginRequest);

        Task<UserResponse> CreateUserAsync(UserRequest user);

        Task<bool> UpdateAdminAsync(int idUser, bool admin);

        Task<bool> SetUserStatusAsync(int idUser, bool active);
    }
}
