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
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        QuizAPIDbContext _context;
        public UserReadRepository(QuizAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<User> Table => _context.Set<User>();

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await Table.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
