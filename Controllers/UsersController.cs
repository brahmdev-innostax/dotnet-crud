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

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserRequest requst)
        {
            return await _service.AddUser(requst);
        }
    }
}
