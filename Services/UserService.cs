using DotNetCRUD_8.Models;
using DotNetCRUD_8.Repositories;
using DotNetCRUD_8.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCRUD_8.Services
{
    public class UserService
    {
        UserRepository _repository;

        public UserService(UserRepository repository) 
        {
            this._repository = repository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public async Task<User> AddUser(CreateUserRequest request)
        {
            return await _repository.AddUser(request);
        }
    }
}
