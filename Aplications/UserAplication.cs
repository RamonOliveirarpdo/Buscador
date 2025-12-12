using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Repositories;

namespace Buscador.Aplications
{
    public class UserAplication : IUserAplication
    {
        private readonly IUserRepository _userRepository;

        public UserAplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserNameAsync(int userId)
        {
            if (userId == 0)
            {
                throw new InvalidOperationException("User não está cadastrado no sistema.");
            }

            var data = await _userRepository.GetUserByIdAsync(userId);

            var userDto = new UserDto
            {
                Id = data.Id,
                UserName = data.UserName,
                Email = data.Email,
                Password = data.Password,
                EmailConfirmed = data.EmailConfirmed,
                PasswordValid = data.PasswordValid,
                CreatedAt = data.CreatedAt,
                IsAdmin = data.IsAdmin,
                IsActive = data.IsActive
            };
            
            return userDto;
        }
    }
}
