using Buscador.Dtos;

namespace Buscador.Interfaces
{
    public interface IUserAplication
    {
        Task<UserDto> GetUserNameAsync(int userId);
    }
}
