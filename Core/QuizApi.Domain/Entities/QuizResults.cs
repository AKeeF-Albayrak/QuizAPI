using QuizApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Entities
{
    public class QuizResult : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
        public int CorrectAnswers { get; set; }
        public User User { get; set; }
        public Quiz Quiz { get; set; }
    }
}
