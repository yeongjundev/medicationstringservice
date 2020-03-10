using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicationStringService.API.Persistences
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> context;

        public Repository(DbSet<T> dbSetContext)
        {
            context = dbSetContext;
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.AddRange(entities);
        }

        public async ValueTask<IEnumerable<T>> GetAll()
        {
            return await context.ToListAsync();
        }

        public async ValueTask<T> GetById(int id)
        {
            return await context.FindAsync(id);
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            context.RemoveRange(entities);
        }
    }
}