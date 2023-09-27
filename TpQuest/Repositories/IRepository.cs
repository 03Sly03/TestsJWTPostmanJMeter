using System.Linq.Expressions;

namespace TpQuest.Repositories
{
    public interface IRepository<Entity>
    {
        // CREATE

        Task<int> Add(Entity entity);

        // READ

        Task<Entity?> GetById(int id);
        Task<Entity?> Get(Expression<Func<Entity, bool>> predicate);

        Task<List<Entity>> GetAll();
    }
}
