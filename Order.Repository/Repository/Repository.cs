using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.DataAccess.Context;
using Order.DataAccess.Repository.Interfaces;
using Order.Domain.Entity.Base;

namespace Order.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CustomerOrderPostreDbContext _context;

        public Repository(CustomerOrderPostreDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>?> GetAll(CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = this.Include<T>(includes);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetById(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = this.Include<T>(includes);

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken cancellationToken, bool asNoTracking = false, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = this.Include<T>(includes);

            return asNoTracking ? await query.SingleOrDefaultAsync(filter, cancellationToken) :
                await query.AsNoTracking().SingleOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<List<T>?> GetByFilterList(Expression<Func<T, bool>> filter, CancellationToken cancellationToken, bool asNoTracking = false, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = this.Include<T>(includes);

            return asNoTracking ? await query.Where(filter).ToListAsync(cancellationToken) :
                await query.AsNoTracking().Where(filter).ToListAsync(cancellationToken);
        }

        public async Task<T> Create(T entity, CancellationToken cancellationToken)
        {
            return (await _context.Set<T>().AddAsync(entity, cancellationToken)).Entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation)
        {
            return await _context.SaveChangesAsync(cancellation);
        }

        public IQueryable<T> Include<T>(params Expression<Func<T, object>>[] includes)
                                       where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include).AsNoTracking();
            }

            return query == null ? _context.Set<T>() : query;
        }
    }
}
