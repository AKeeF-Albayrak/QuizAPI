using QuizApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Application.Repositories
{
    public interface IQuizReadRepository : IReadRepository<Quiz>
    {
        public Task<List<Quiz>> GetQuizzesByAdminIdAsync(string id);
    }
}
