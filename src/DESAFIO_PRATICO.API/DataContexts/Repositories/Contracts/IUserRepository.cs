using DESAFIO_PRATICO.API.Models;

namespace DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<UserModel> GetByNameAsync(string username);
        Task<UserModel> GetByIdAsync(int id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> CreateAsync(UserModel user);
        Task<UserModel> UpdateAsync(UserModel user, int id);
        Task<bool> DeleteAsync(int id);
    }
}
