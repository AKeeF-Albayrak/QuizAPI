using QuizApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
