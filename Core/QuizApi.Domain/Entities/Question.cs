using QuizApi.Domain.Entities.Common;
using QuizApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Entities
{
    public class Question : BaseEntity
    {
        public Guid QuizId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }

        // Navigation property to link to Quiz
        public Quiz Quiz { get; set; }
    }
}
