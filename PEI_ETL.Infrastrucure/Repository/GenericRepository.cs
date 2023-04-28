using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Interfaces;
using System.Linq.Expressions;

namespace PEI_ETL.Infrastrucure.Repository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ETLDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(
            ETLDbContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = _context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public  void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void Upsert(T entity)
        {
            dbSet.Update(entity);
            
        }
    }
}
