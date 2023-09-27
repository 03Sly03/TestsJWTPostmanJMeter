using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TpQuest.Models;
using TpQuest.Repositories;

namespace TpQuest.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IRepository<Character> _characterRepository;

        public CharacterController(IRepository<Character> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            return Ok(await _characterRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _characterRepository.GetById(id);
            if (character == null)
            {
                return NotFound("Personnel non trouvé");
            }
            return Ok(character);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCharacter([FromBody] Character character)
        {
            int characterId = await _characterRepository.Add(character);

            if (characterId > 0)
            {
                return CreatedAtAction(nameof(GetCharacters), new { id = character.Id, Message = "Personnage ajouté !" });
            }

            return BadRequest("Quelque chose ne va pas");
        }
    }
}
