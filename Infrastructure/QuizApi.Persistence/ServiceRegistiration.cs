using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.Application.Repositories;
using QuizApi.Persistence.Contexts;
using QuizApi.Persistence.Repositories;
using QuizAPI.Application.Repositories;
using QuizAPI.Persistence.Repositories;
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
            services.AddDbContext<QuizAPIDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")), ServiceLifetime.Scoped);

            services.AddScoped<IAdminReadRepository, AdminReadRepository>();
            services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IAnswerReadRepository, AnswerReadRepository>();
            services.AddScoped<IAnswerWriteRepository, AnswerWriteRepository>();
            services.AddScoped<IQuestionReadRepository, QuestionReadRepository>();
            services.AddScoped<IQuestionWriteRepository, QuestionWriteRepository>();
            services.AddScoped<IQuizReadRepository, QuizReadRepository>();
            services.AddScoped<IQuizWriteRepository, QuizWriteRepository>();
            services.AddScoped<IQuizResultReadRepository, QuizResultReadRepository>();
            services.AddScoped<IQuizResultWriteRepository, QuizResultWriteRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

        }
    }
}
