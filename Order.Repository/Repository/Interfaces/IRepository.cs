using Order.Domain.Entity.Base;
using System.Linq.Expressions;

namespace Order.DataAccess.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>?> GetAll(CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes);

        Task<T?> GetById(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes);

        Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken cancellationToken, bool asNoTracking = false, params Expression<Func<T, object>>[]? includes);
        Task<List<T>?> GetByFilterList(Expression<Func<T, bool>> filter, CancellationToken cancellationToken, bool asNoTracking = false, params Expression<Func<T, object>>[]? includes);
        Task<T> Create(T entity, CancellationToken cancellationToken);

        void Update(T entity);

        void Remove(T entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
