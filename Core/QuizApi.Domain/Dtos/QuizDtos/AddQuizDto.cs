using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApi.Domain.Enums;

namespace QuizApi.Domain.Dtos.QuizDtos
{
    public class AddQuizDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuizCategory Catagory { get; set; }
        public int Duration { get; set; }

    }
}
