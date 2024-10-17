using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using QuizApi.Domain.StaticVariables;
using QuizApi.Application.Repositories;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(IUserReadRepository userReadRepository, IAdminReadRepository adminReadRepository, ITokenRepository tokenRepository)
        {
            _userReadRepository = userReadRepository;
            _adminReadRepository = adminReadRepository;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userReadRepository.GetUserByUsernameAsync(model.Username);
            if (user != null && user.Password == model.Password)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

                var token = GenerateToken(claims);
                return Ok(new { token });
            }

            var admin = await _adminReadRepository.GetAdminByUsernameAsync(model.Username);
            if (admin != null && admin.Password == model.Password)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

                var token = GenerateToken(claims);
                return Ok(new { token });
            }

            return Unauthorized("Kullanıcı adı veya şifre hatalı.");
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticVariables.SecureKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _tokenRepository.InvalidateToken(token);
            return Ok(new { message = "Logout successful!" });
        }
    }
}
