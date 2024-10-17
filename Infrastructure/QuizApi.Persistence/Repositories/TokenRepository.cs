using QuizApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Persistence.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly HashSet<string> _invalidTokens = new HashSet<string>();

        // Token'ı geçersiz kılma
        public void InvalidateToken(string token)
        {
            _invalidTokens.Add(token);
        }

        // Token'ın geçerli olup olmadığını kontrol etme
        public bool IsTokenValid(string token)
        {
            return !_invalidTokens.Contains(token);
        }
    }
}
