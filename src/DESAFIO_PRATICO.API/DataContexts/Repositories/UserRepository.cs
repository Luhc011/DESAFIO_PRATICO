using DESAFIO_PRATICO.API.DataContexts.Data;
using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Exceptions;
using DESAFIO_PRATICO.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO_PRATICO.API.DataContexts.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(TaskDbContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
            => await _context.Users!.ToListAsync();

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var users = await _context.Users!.FirstOrDefaultAsync(u => u.Id == id);

            if (users is null)
            {
                _logger.LogInformation("User id {Id}, not found", id);
                throw new UserNotFoundException($"User id {id}, not found");
            }

            return users;
        }

        public async Task<UserModel> GetByNameAsync(string username)
        {
            var users = await _context.Users!.FirstOrDefaultAsync(u => u.Name == username);

            if (users is null)
            {
                _logger.LogInformation("User with username {Username} not found", username);
                throw new UserNotFoundException($"User with username {username} not found");
            }

            return users;
        }

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            await _context.Users!.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateAsync(UserModel user, int id)
        {
            var userExists = await GetByIdAsync(id);

            if (userExists != null)
            {
                _context.Entry(userExists).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }

            return userExists is null
                ? throw new UserNotFoundException($"User id {id}, not found")
                : userExists;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id) ?? throw new UserNotFoundException($"User id {id}, not found");

            _context.Users!.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
