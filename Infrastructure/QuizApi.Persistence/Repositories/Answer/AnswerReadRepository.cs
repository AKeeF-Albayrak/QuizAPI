using Microsoft.EntityFrameworkCore;
using QuizApi.Domain.Entities;
using QuizApi.Persistence.Contexts;
using QuizAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Persistence.Repositories
{
    public class AnswerReadRepository : ReadRepository<Answer>, IAnswerReadRepository
    {
        QuizAPIDbContext _context;
        public AnswerReadRepository(QuizAPIDbContext context) : base(context)
        {
            _context =context;
        }

        public DbSet<Answer> Table => _context.Set<Answer>();

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid questionId))
            {
                throw new ArgumentException("Invalid question ID format.");
            }

            // QuizId'ye göre soruları filtreleyip getiriyoruz
            var answers = await Table
                                 .Where(q => q.QuestionId == questionId)
                                 .ToListAsync();

            return answers;
        }
    }
}
