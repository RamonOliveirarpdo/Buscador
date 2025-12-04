using Buscador.Core.Interfaces;
using Buscador.Dtos;
using Buscador.Dtos.Requests;
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
            var data = await _userRepository.GetUserByIdAsync(userId);
            if (data != null)
            {
                throw new InvalidOperationException("User já cadastrado no sistema.");
            }

            var userResponse = CriaUserResponse(data);

            return userResponse;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(userRequest.Email);
            if (user != null)
            {
                throw new InvalidOperationException("Não foi possível concluir o cadastro com os dados fornecidos.");
            }

            user = CriaUser(userRequest);
            var data = await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            var userResponse = CriaUserResponse(data);

            return userResponse;
        }

        public async Task<bool> UpdateAdminAsync(int idUser, bool admin)
        {
            var user = await _userRepository.GetUserByIdAsync(idUser);
            if (!user.IsActive)
            {
                throw new InvalidOperationException("Usuário inativo não pode ser promovido a admin.");
            }

            user.IsAdmin = admin;
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetUserStatusAsync(int idUser, bool isActive)
        {
            var user = await _userRepository.GetUserByIdAsync(idUser);
            if (user == null)
            {
                return false;
            }

            if (user.IsAdmin)
            {
                throw new InvalidOperationException("Usuário admin não pode ter seu status alterado.");
            }

            user.IsActive = isActive;

            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.LoginAsync(loginRequest);
            if (user == null || !user.IsActive) 
            {
                throw new InvalidOperationException("Credenciais inválidas.");
            }

            var result = _hashService.VerifyPassword(user.Password, loginRequest.Password);

            if (result != "Valid")
            {
                throw new InvalidOperationException("Usuário ou senha inválidos.");
            }

            return true;
        }

        private User CriaUser(UserRequest userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = _hashService.EncriptPassword(userDto.Password),
                CreatedAt = DateTime.UtcNow,
                IsAdmin = false,
                IsActive = true,
                EmailConfirmed = true,
                PasswordValid = true
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
                Password = _hashService.EncriptPassword(user.Password),
                CreatedAt = user.CreatedAt,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive
            };

            return userResponse;
        }
    }
}
