using Microsoft.EntityFrameworkCore;
using TpQuest.Models;

namespace TpQuest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Character> Characters { get; set; } 
    }
}
