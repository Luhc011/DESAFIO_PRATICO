using DESAFIO_PRATICO.API.Models;

namespace DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts
{
    public interface ITaskRepository
    {
        Task<TaskModel> GetByIdAsync(int id);
        Task<IEnumerable<TaskModel>> GetAllAsync();
        Task<TaskModel> CreateAsync(TaskModel task);
        Task<TaskModel> UpdateAsync(TaskModel task, int id);
        Task<bool> DeleteAsync(int id);
    }
}
