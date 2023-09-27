using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using TpQuest.DTOs;
using TpQuest.Helpers;
using TpQuest.Models;
using TpQuest.Repositories;

namespace TpQuest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppSettings _settings;
        public AuthenticationController(IRepository<User> userRepository, IOptions<AppSettings> optionAppSettings)
        {
            _userRepository = userRepository;
            _settings = optionAppSettings.Value;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _userRepository.Get(u => u.Email == user.Email) != null)
            {
                return BadRequest("Le mail est déjà utilisé.");
            }

            if (await _userRepository.Add(user) > 0)
            {
                return Ok(new {id = user.Id, Message = "Utilisateur créé"});
            }
            return BadRequest("Quelque chose ne va pas...");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO login)
        {
            var user = await _userRepository.Get(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                return NotFound("Utilisateur inconny");
            }

            //JWT
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            SigningCredentials signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey!)),
            SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(7)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                Token = token,
                Message = "Authentification validé",
                User = user
            });
        }
    }
}
