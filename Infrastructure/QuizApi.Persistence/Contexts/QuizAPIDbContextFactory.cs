using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizAPI.Persistence;

namespace QuizApi.Persistence.Contexts
{
    public class QuizAPIDbContextFactory : IDesignTimeDbContextFactory<QuizAPIDbContext>
    {
        public QuizAPIDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuizAPIDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SQLServer"));

            return new QuizAPIDbContext(optionsBuilder.Options);
        }
    }
}
