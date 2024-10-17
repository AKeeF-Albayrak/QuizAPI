using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class QuestionReadRepository : ReadRepository<Question>, IQuestionReadRepository
    {
        QuizAPIDbContext _context;
        public QuestionReadRepository(QuizAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<Question> Table => _context.Set<Question>();

        public async Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid quizId))
            {
                throw new ArgumentException("Invalid quiz ID format.");
            }

            // QuizId'ye göre soruları filtreleyip getiriyoruz
            var questions = await Table
                                 .Where(q => q.QuizId == quizId)
                                 .ToListAsync();

            return questions;
        }

    }
}
