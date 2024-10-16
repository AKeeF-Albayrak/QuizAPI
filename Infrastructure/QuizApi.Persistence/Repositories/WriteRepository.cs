using Microsoft.EntityFrameworkCore;
using QuizApi.Domain.Entities.Common;
using QuizApi.Persistence.Contexts;
using QuizAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly QuizAPIDbContext _context;

        public WriteRepository(QuizAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task AddAsync(T model) => await Table.AddAsync(model);

        public async Task AddRangeAsync(IEnumerable<T> models) => await Table.AddRangeAsync(models);

        public async Task UpdateAsync(T model)
        {
            Table.Update(model);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T model)
        {
            Table.Remove(model);
            await Task.CompletedTask;
        }
        public async Task<int> SaveChangesAsync() =>await _context.SaveChangesAsync();
    }
}
