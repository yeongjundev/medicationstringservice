using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicationStringService.API.Persistences
{
    public interface IRepository<T> where T : class
    {
        ValueTask<T> GetById(int id);

        ValueTask<IEnumerable<T>> GetAll();

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}