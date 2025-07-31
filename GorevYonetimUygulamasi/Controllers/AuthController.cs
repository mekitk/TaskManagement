using Görev_Yönetim_Uygulaması.Dto;
using Görev_Yönetim_Uygulaması.Models;
using Görev_Yönetim_Uygulaması.Services;
using Microsoft.AspNetCore.Mvc;

namespace Görev_Yönetim_Uygulaması.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(AuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Email veya kullanıcı adı zaten var mı?
            var existing = await _authService.GetByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest("Bu e-posta adresiyle zaten bir kullanıcı var.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Developer"
            };

            await _authService.RegisterAsync(user);
            return Ok("Kayıt başarılı");
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new
                {
                    error = true,
                    status = 401,
                    message = "Geçersiz e-posta veya şifre."
                });

            var token = _jwtService.GenerateToken(user);

            // Kullanıcı bilgilerini sadeleştirilmiş haliyle döndür
            var userResponse = new
            {
                id = user.Id,
                email = user.Email,
                name = user.Username,
                role = user.Role,
            };

            return Ok(new { token, user = userResponse });
        }
    }
}