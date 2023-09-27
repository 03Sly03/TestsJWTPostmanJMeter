using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TpQuest.Data;
using TpQuest.Models;

namespace TpQuest.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationDbContext _dbContext { get; }
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // CREATE

        public async Task<int> Add(User user)
        {
            var addedObj = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.Id;
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User?> Get(Expression<Func<User, bool>> predicate)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
