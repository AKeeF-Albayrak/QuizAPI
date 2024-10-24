using QuizApi.Domain.Entities.Common;
using QuizApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuizCategory Category { get; set; }
        public int Duration { get; set; }
        public Guid AdminId { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}
