using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Datingapp.API.Data;
using Datingapp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Datingapp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;

        }

        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var userstoReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(userstoReturn);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.getUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }



    }
}