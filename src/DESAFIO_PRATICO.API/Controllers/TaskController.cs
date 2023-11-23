using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIO_PRATICO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        [HttpGet]
        public async Task<ActionResult<TaskModel>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task)
        {
            var taskCreated = await _taskRepository.CreateAsync(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskCreated.Id }, taskCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTask(TaskModel task, int id)
        {
            var taskUpdated = await _taskRepository.UpdateAsync(task, id);

            return Ok(taskUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            var taskDeleted = await _taskRepository.DeleteAsync(id);

            return Ok(taskDeleted);
        }
    }
}
