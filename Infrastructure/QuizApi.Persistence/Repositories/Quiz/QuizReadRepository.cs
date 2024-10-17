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
    public class QuizReadRepository : ReadRepository<Quiz>, IQuizReadRepository
    {
        QuizAPIDbContext _context;
        public QuizReadRepository(QuizAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<Quiz> Table => _context.Set<Quiz>();
        

        public async Task<List<Quiz>> GetQuizzesByAdminIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                {
                    throw new ArgumentException("Invalid admin ID format.");
                }
                var quizzes = await Table.Where(q => q.AdminId == guidId).ToListAsync();

                return quizzes;
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving quizzes: {ex.Message}");
            }
        }
    }
}
