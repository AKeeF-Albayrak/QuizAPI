using QuizApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        // Navigation property to link to Question
        public Question Question { get; set; }
    }
}
