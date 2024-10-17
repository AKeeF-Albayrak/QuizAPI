using QuizApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Application.Repositories
{
    public interface IQuestionReadRepository : IReadRepository<Question>
    {
        public Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(string id);
    }
}
