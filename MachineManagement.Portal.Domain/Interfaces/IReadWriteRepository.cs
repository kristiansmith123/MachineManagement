using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineManagement.Portal.Domain.Interfaces
{
    public interface IReadWriteRepository<T>
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T ientity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
