using DESAFIO_PRATICO.API.DataContexts.Repositories.Contracts;
using DESAFIO_PRATICO.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIO_PRATICO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("name/{username}")]
        public async Task<ActionResult<UserModel>> GetUserByName(string username)
        {
            var user = await _userRepository.GetByNameAsync(username);

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            var userCreated = await _userRepository.CreateAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = userCreated.Id }, userCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser(UserModel user, int id)
        {
            var userUpdated = await _userRepository.UpdateAsync(user, id);

            return Ok(userUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var userDeleted = await _userRepository.DeleteAsync(id);

            return Ok(userDeleted);
        }
    }
}
