using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Domain.Entities;
using QuizAPI.Application.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace QuizApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly string _secretKey;

        public AuthController(IUserReadRepository userReadRepository, IAdminReadRepository adminReadRepository)
        {
            _userReadRepository = userReadRepository;
            _adminReadRepository = adminReadRepository;
            _secretKey = GenerateSecureKey();
        }

        // Giriş metodu
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Önce kullanıcıyı kontrol et
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

            // Ardından admini kontrol et
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0iABIYPO/aKD9vnMEtoPzJM+9Tn7hrUrBmClVKSoo1o="));
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
        public static string GenerateSecureKey(int size = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[size];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

    }
}
