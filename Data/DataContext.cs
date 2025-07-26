using DotNetCRUD_8.Models;
using DotNetCRUD_8.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotNetCRUD_8.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } // using just get and set will allow accessing and modifying it from outside
    }
}
