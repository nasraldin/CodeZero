using CodeZero.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeZero.Data
{
    public interface IRepositoryAsync<T> where T : Entity, IAggregateRoot
    {
        Task<IReadOnlyList<T>> ListAllAsync();

        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);


        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        //Task<int> CountAsync(ISpecification<T> spec);

        //Task<T> FirstAsync(ISpecification<T> spec);

        //Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
