using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionReadRepository _questionReadRepository;
        private readonly IQuestionWriteRepository _questionWriteRepository;

        public QuestionsController(IQuestionReadRepository questionReadRepository, IQuestionWriteRepository questionWriteRepository)
        {
            _questionReadRepository = questionReadRepository;
            _questionWriteRepository = questionWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionsByQuizId(string id)
        {
            var questions = await _questionReadRepository.GetQuestionsByQuizIdAsync(id);
            return Ok(questions);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuestionAsync([FromBody] Question question)
        {
            await _questionWriteRepository.AddAsync(question);
            await _questionWriteRepository.SaveChangesAsync();
            return Ok(question);
        }
    }
}
