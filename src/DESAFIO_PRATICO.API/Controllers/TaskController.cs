using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Exceptions;
using DESAFIO_PRATICO.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIO_PRATICO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                return Ok(task);
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogInformation(ex, "Task not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting a task by ID.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var taskCreated = await _taskRepository.CreateAsync(task);
                return CreatedAtAction(nameof(GetTaskById), new { id = taskCreated.Id }, taskCreated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a task.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTask(TaskModel task, int id)
        {
            try
            {
                var taskUpdated = await _taskRepository.UpdateAsync(task, id);
                return Ok(taskUpdated);
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogInformation(ex, "Task not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a task.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            try
            {
                var taskDeleted = await _taskRepository.DeleteAsync(id);
                return Ok(taskDeleted);
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogInformation(ex, "Task not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a task.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
