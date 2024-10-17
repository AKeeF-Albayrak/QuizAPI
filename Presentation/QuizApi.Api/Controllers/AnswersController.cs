using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;
using QuizAPI.Persistence.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerReadRepository _answerReadRepository;
        private readonly IAnswerWriteRepository _answerWriteRepository;

        public AnswersController(IAnswerReadRepository answerReadRepository, IAnswerWriteRepository answerWriteRepository) 
        {
            _answerReadRepository = answerReadRepository;
            _answerWriteRepository = answerWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswersByQuestionId(string id)
        {
            var questions = await _answerReadRepository.GetAnswersByQuestionIdAsync(id);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            await _answerWriteRepository.AddAsync(answer);
            await _answerWriteRepository.SaveChangesAsync();
            return Ok(answer);  
        }


    }
}
