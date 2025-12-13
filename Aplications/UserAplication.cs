using Buscador.Core.Interfaces;
using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Models;

namespace Buscador.Aplications
{
    public class UserAplication : IUserAplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public UserAplication(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<UserResponse> GetUserNameAsync(int userId)
        {
            if (userId == 0)
            {
                throw new InvalidOperationException("User não está cadastrado no sistema.");
            }

            var data = await _userRepository.GetUserByIdAsync(userId);
            var userResponse = CriaUserResponse(data);

            return userResponse;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest userRequest)
        {
            var data = CriaUser(userRequest);
            data = await _userRepository.AddUserAsync(data);

            int recordsAffected = await _userRepository.SaveChangesAsync();

            if (recordsAffected == 0)
            {
                throw new Exception("Falha ao persistir a nova situação no banco de dados.");
            }

            var userResponse = CriaUserResponse(data);

            return userResponse;
        }

        public User CriaUser(UserRequest userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = _hashService.HashPassword(userDto.Password),
                CreatedAt = DateTime.UtcNow,
                IsAdmin = false,
                IsActive = true
            };

            return user;
        }

        public UserResponse CriaUserResponse(User user)
        {
            var userResponse = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = _hashService.HashPassword(user.Password),
                CreatedAt = user.CreatedAt,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive
            };

            return userResponse;
        }
    }
}
