﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Dtos.QuizDtos;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuiz([FromBody] AddQuizDto dto)
        {
            var adminIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            Guid adminId = Guid.Parse(adminIdClaim.Value);

            Quiz quiz = new Quiz 
            { 
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Catagory,
                Duration = dto.Duration,
                AdminId = adminId,
            };
            await _quizWriteRepository.AddAsync(quiz);
            await _quizWriteRepository.SaveChangesAsync();
            return Ok("Quiz Added Succsessfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizzessByAdminIdAsync(string id)
        {
            try
            {
                // GetQuizzesByAdminId metodunu çağırıyoruz
                var quizzes = await _quizReadRepository.GetQuizzesByAdminIdAsync(id);

                // Eğer quiz bulunamazsa 404 Not Found döndürüyoruz
                if (quizzes == null || !quizzes.Any())
                {
                    return NotFound("No quizzes found for the given admin ID.");
                }

                // Başarılıysa 200 OK ve quiz listesini döndürüyoruz
                return Ok(quizzes);
            }
            catch (ArgumentException ex)
            {
                // Geçersiz input için 400 Bad Request döndürüyoruz
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Genel hatalar için 500 Internal Server Error döndürüyoruz
                return StatusCode(500, $"An error occurred while retrieving quizzes: {ex.Message}");
            }
        }

    }
}
