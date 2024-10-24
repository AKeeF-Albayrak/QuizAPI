using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApi.Domain.Enums;

namespace QuizApi.Domain.Dtos.QuestionDtos
{
    public class AddQuestionDto
    {
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public Guid QuizId { get; set; }
    }
}
