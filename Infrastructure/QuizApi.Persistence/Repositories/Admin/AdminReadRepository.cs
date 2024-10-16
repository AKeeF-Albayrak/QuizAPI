﻿using QuizApi.Domain.Entities;
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
        public AdminReadRepository(QuizAPIDbContext context) : base(context)
        {
        }
    }
}
