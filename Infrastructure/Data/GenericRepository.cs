using Core.Entities;
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
    }
}