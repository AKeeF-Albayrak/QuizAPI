using QuizApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Application.Repositories
{
    public interface IAnswerReadRepository : IReadRepository<Answer>
    {
        public Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(string id);
    }
}
