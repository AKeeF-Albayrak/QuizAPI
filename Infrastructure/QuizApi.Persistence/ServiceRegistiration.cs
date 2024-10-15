using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPI.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<QuizAPIDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")),ServiceLifetime.Scoped);
            
        }
    }
}
