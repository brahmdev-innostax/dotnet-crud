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

        public async Task<User> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }

        public async Task<User> AddUser(CreateUserRequest request)
        {
            return await _repository.AddUser(request);
        }

        public async Task<User> UpdateUser(int id, CreateUserRequest request)
        {
            return await _repository.UpdateUser(id, request);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _repository.DeleteUser(id);
        }
    }
}
