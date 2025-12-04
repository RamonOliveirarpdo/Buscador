using Buscador.Dtos.Requests;
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

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            return user;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .Where(u => u.UserName == loginRequest.UserName)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
