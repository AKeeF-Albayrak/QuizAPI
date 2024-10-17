using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly IAdminWriteRepository _adminWriteRepository;

        public AdminsController(IAdminWriteRepository adminWriteRepository, IAdminReadRepository adminReadRepository)
        {
            _adminReadRepository = adminReadRepository;
            _adminWriteRepository = adminWriteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
        {
            await _adminWriteRepository.AddAsync(admin);
            await _adminWriteRepository.SaveChangesAsync();
            return Ok("Admin basariyla eklendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            Guid guidId;

            if (Guid.TryParse(id, out guidId))
            {
                Admin admin = await _adminReadRepository.GetByIdAsync(guidId);
                if (admin == null)
                {
                    return NotFound("Can not Find an Id Like This!");
                }
                else
                {
                    return Ok(admin);
                }
            }
            else
            {
                return NotFound("Invalid Id");
            }
        }


    }
}
