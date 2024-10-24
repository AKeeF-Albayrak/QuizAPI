﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;
using QuizAPI.Persistence.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly private IUserReadRepository _userReadRepository;
        readonly private IUserWriteRepository _userWriteRepository;

        public UsersController(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            Guid guidId;

            if (Guid.TryParse(id, out guidId))
            {
                User user = await _userReadRepository.GetByIdAsync(guidId);
                if (user == null)
                {
                    return NotFound("Can not Find an Id Like This!");
                }
                else
                {
                    return Ok(user);
                }
            }
            else
            {
                return NotFound("Invalid Id");
            }
        }
    }
}
