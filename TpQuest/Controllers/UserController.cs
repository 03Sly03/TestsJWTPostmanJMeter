using Microsoft.AspNetCore.Mvc;
using TpQuest.Models;
using TpQuest.Repositories;

namespace TpQuest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("Personnel non trouvé");
            }
            return Ok(user);
        }
    }
}
