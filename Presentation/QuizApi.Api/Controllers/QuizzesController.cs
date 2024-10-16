using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizReadRepository _quizReadRepository;
        private readonly IQuizWriteRepository _quizWriteRepository;


        public QuizzesController(IQuizReadRepository quizReadRepository, IQuizWriteRepository quizWriteRepository)
        {
            _quizReadRepository = quizReadRepository;
            _quizWriteRepository = quizWriteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuiz(Quiz quiz)
        {
            await _quizWriteRepository.AddAsync(quiz);
            await _quizWriteRepository.SaveChangesAsync();
            return Ok("Quiz Added Succsessfully");
        }
    }
}
