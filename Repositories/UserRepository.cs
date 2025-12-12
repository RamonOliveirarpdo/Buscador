using Buscador.Dtos;
using Buscador.Infrastructure.Data;
using Buscador.Interfaces;
using Buscador.Models;
using Microsoft.EntityFrameworkCore;

namespace Buscador.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(UserRequest User)
        {
            var resultado = new User
            {
                UserName = User.UserName,
                Email = User.Email,
                Password = User.Password,
                CreatedAt = User.CreatedAt,
                IsActive = User.IsActive
            };

            return resultado;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            IQueryable<User> query = _context.Users;
            query = _context.Users.Where(u => u.Id == userId);

            var resultado = await query
                 .Where(s => s.IsActive == true)
                 .Select(s => new User
                 {
                     Id = s.Id,
                     UserName = s.UserName,
                     Email = s.Email
                 }
                 ).FirstOrDefaultAsync();

            return resultado;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            return user;
        }

        public async Task<int> SaveChangesAsync()
        {
            var response = await _context.SaveChangesAsync();
            return response;
        }
    }
}
