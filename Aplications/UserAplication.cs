using Buscador.Core.Interfaces;
using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Models;
using Buscador.Repositories;
using Microsoft.EntityFrameworkCore;

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

            var userResponse = new UserResponse
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

            return userResponse;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest userDto)
        {
            var data = await _userRepository.CreateUserAsync(userDto);

            var user = new User
            {
                Id = data.Id,
                UserName = data.UserName,
                Email = data.Email,
                Password = _hashService.HashPassword(data.Password),
                EmailConfirmed = data.EmailConfirmed,
                PasswordValid = data.PasswordValid,
                CreatedAt = data.CreatedAt,
                IsAdmin = data.IsAdmin,
                IsActive = data.IsActive
            };

            user = await _userRepository.AddUserAsync(user);
            int linhasAfetadas = await _userRepository.SaveChangesAsync();
            if (linhasAfetadas == 0)
            {
                throw new Exception("Falha ao persistir a nova situação no banco de dados.");
            }
            var userDtoResponse = CriaUserDto(user);
            var userResponse = CriaUserResponse(userDtoResponse);

            return userResponse;
        }

        public UserDto CriaUserDto(User user)
        {
            var userDto = new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = _hashService.HashPassword(user.Password),
                CreatedAt = user.CreatedAt,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive
            };

            return userDto;
        }

        public User CriaUser(UserRequest userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = _hashService.HashPassword(userDto.Password),
                CreatedAt = userDto.CreatedAt,
                IsActive = userDto.IsActive
            };

            return user;
        }

        public UserResponse CriaUserResponse(UserDto userDto)
        {
            var userResponse = new UserResponse
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = _hashService.HashPassword(userDto.Password),
                CreatedAt = userDto.CreatedAt,
                IsAdmin = userDto.IsAdmin,
                IsActive = userDto.IsActive
            };

            return userResponse;
        }
    }
}
