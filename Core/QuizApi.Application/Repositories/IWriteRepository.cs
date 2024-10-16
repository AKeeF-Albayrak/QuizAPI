using QuizApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T model);
        Task AddRangeAsync(IEnumerable<T> models);
        Task UpdateAsync(T model);
        Task DeleteAsync(T model);
        Task<int> SaveChangesAsync();
    }
}
