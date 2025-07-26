using DotNetCRUD_8.Data;
using DotNetCRUD_8.Models;
using DotNetCRUD_8.Requests;
using Microsoft.EntityFrameworkCore;

namespace DotNetCRUD_8.Repositories
{
    public class UserRepository
    {
        // an unused instance variable to show the use of custom logic in getter and setter
        public string Name { get { 
            if(true)
                {
                    return Name;
                }
            else
                {
                    throw new UnauthorizedAccessException("You mf cannot access this!");
                }
            }
            
            set
            {
                if( value.Contains("mafia69"))
                {
                    Name = value;
                }
            }
        }


        public UserRepository(DataContext context)
        {
            _context = context;
        }
        private readonly DataContext _context;

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddUser(CreateUserRequest request)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(int id, CreateUserRequest update)
        {
            var user = await GetUser(id);
            if(user is not null)
            {
                user.Email = update.Email;
                user.Name = update.Name;
                user.Phone = update.Phone;
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUser(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
