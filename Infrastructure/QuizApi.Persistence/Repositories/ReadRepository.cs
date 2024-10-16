using QuizAPI.Application.Repositories;
using QuizApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QuizAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly QuizAPIDbContext _context;

        public ReadRepository(QuizAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public async Task<T> GetByIdAsync(Guid id) => await Table.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await Table.ToListAsync();
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) => await Table.Where(predicate).ToListAsync();
        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate) => await Table.FirstOrDefaultAsync(predicate);
        public async Task<int> CountAsync() => await Table.CountAsync();
    }
}
