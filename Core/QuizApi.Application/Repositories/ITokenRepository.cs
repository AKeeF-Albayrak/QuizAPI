using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Application.Repositories
{
    public interface ITokenRepository
    {
        void InvalidateToken(string token);
        bool IsTokenValid(string token);
    }
}
