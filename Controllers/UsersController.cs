using DotNetCRUD_8.Models;
using DotNetCRUD_8.Requests;
using DotNetCRUD_8.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCRUD_8.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService userService)
        {
            _service = userService;       
        }

        [HttpGet("health")]
        public string Hello()
        {
            return "Hi there! Server is up and running!";
        }

        [HttpGet("dummy")]
        public async Task<ActionResult<User>> DummyUser()
        {
            return new User()
            {
                Name = "Dummy",
                Email = "dummy@test.com",
                Phone = "+10000000000"
            };
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _service.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetUser(id);
            if (user is null)
                return NotFound("User does not exist");
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserRequest requst)
        {
            return await _service.AddUser(requst);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, CreateUserRequest request)
        {
            var user = await _service.UpdateUser(id, request);
            if (user is null)
                return NotFound("User does not exist!");
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var deleted = await _service.DeleteUser(id);
            if (!deleted)
                return BadRequest("Not found!");
            return Ok("Success!");
        }
    }
}
