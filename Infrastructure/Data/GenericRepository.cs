using Core.Entities;
using Core.Specifications;
using Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        => _context = context;

        public async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).ToListAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

    }
}