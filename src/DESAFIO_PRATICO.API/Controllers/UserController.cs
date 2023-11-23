using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Exceptions;
using DESAFIO_PRATICO.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIO_PRATICO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation(ex, "User not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting a user by ID.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("name/{username}")]
        public async Task<ActionResult<UserModel>> GetUserByName(string username)
        {
            try
            {
                var user = await _userRepository.GetByNameAsync(username);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation(ex, "User not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting a user by name.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userCreated = await _userRepository.CreateAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = userCreated.Id }, userCreated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a user.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser(UserModel user, int id)
        {
            try
            {
                var userUpdated = await _userRepository.UpdateAsync(user, id);
                return Ok(userUpdated);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation(ex, "User not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a user.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            try
            {
                var userDeleted = await _userRepository.DeleteAsync(id);
                return Ok(userDeleted);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation(ex, "User not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
