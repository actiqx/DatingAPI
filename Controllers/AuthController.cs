using System.Threading.Tasks;
using Datingapp.API.Data;
using Datingapp.API.DTOs;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Datingapp.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthInterface _repo;

        public AuthController(IAuthInterface repo)
        {
            this._repo = repo;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]userForRegisterDto userdto)
        {
            //Validate request

            userdto.username = userdto.username.ToLower();
            if (await _repo.UserExists(userdto.username))
            {
                return BadRequest("Username is already taken");
            }
            var userToCreate = new User
            {
                UserName = userdto.username

            };

            var createUser = await _repo.Register(userToCreate, userdto.password);
            return StatusCode(201);
            //return CreatedAtRoute();
        }
    }
}