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
    public class AdminReadRepository : ReadRepository<Admin>, IAdminReadRepository
    {
        QuizAPIDbContext _context;
        public AdminReadRepository(QuizAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<Admin> Table => _context.Set<Admin>();
        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            return await Table.SingleOrDefaultAsync(a => a.Username == username);
        }
    }
}
