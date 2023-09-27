using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TpQuest.Data;
using TpQuest.Models;

namespace TpQuest.Repositories
{
    public class CharacterRepository : IRepository<Character>
    {
        private ApplicationDbContext _dbContext { get; }
        public CharacterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // CREATE

        public async Task<int> Add(Character character)
        {
            var addedObj = await _dbContext.Characters.AddAsync(character);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.Id;
        }

        public async Task<Character?> GetById(int id)
        {
            return await _dbContext.Characters.FindAsync(id);
        }

        public async Task<Character?> Get(Expression<Func<Character, bool>> predicate)
        {
            return await _dbContext.Characters.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Character>> GetAll()
        {
            return await _dbContext.Characters.ToListAsync();
        }
    }
}
