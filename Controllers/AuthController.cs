using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Datingapp.API.Data;
using Datingapp.API.DTOs;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Datingapp.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthInterface _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthInterface repo, IConfiguration config)
        {
            this._config = config;
            this._repo = repo;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] userForRegisterDto userdto)
        {
            if (!string.IsNullOrEmpty(userdto.username))
                userdto.username = userdto.username.ToLower();
            if (await _repo.UserExists(userdto.username))
            {
                ModelState.AddModelError("Username", "Username already exist");
            }
            //Validate request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                UserName = userdto.username

            };

            var createUser = await _repo.Register(userToCreate, userdto.password);
            return StatusCode(201);
            //return CreatedAtRoute();
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] userForLoginDto userlogindto)
        {

            var userFromRepo = await _repo.Login(userlogindto.username.ToLower(), userlogindto.password);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userFromRepo.id.ToString()),
                    new Claim(ClaimTypes.Name,userFromRepo.UserName)
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }
    }
}