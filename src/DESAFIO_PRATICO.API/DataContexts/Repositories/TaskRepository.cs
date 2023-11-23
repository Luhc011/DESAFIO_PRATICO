using DESAFIO_PRATICO.API.DataContexts.Data;
using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO_PRATICO.API.DataContexts.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context) => _context = context;

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
            => await _context.Tasks!.Include(u => u.User).ToListAsync();

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var task = await _context.Tasks!.Include(u => u.User).FirstOrDefaultAsync(t => t.Id == id);

            return task is null
                ? throw new Exception("Task not found")
                : task;
        }

        public async Task<TaskModel> CreateAsync(TaskModel task)
        {
            await _context.Tasks!.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateAsync(TaskModel task, int id)
        {
            var taskExists = await GetByIdAsync(id);

            if (taskExists != null)
            {
                _context.Entry(taskExists).CurrentValues.SetValues(task);
                await _context.SaveChangesAsync();
            }

            return taskExists is null
                ? throw new Exception($"Task id {id}, not found")
                : taskExists;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await GetByIdAsync(id) ?? throw new Exception($"Task id {id}, not found");

            _context.Tasks!.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
