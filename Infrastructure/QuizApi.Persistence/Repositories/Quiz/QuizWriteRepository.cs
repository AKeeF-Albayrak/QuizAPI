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
    public class QuizWriteRepository : WriteRepository<Quiz>, IQuizWriteRepository
    {
        private readonly DbContext _context;
        public QuizWriteRepository(QuizAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<Admin> TableAdmin => _context.Set<Admin>();
        public DbSet<Quiz> Table => _context.Set<Quiz>();   

        public async Task AddAsync(Quiz quiz)
        {
            var existingAdmin = await TableAdmin.FindAsync(quiz.AdminId);
            if (existingAdmin == null)
            {
                throw new InvalidOperationException("Admin does not exist.");
            }
            await Table.AddAsync(quiz);
        }
    }
}
