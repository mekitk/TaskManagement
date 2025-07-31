using Görev_Yönetim_Uygulaması.Services;
using System.Security.Claims;

namespace Görev_Yönetim_Uygulaması.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;

        public JwtMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context, AuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var userId = _jwtService.ValidateToken(token);
                if (userId != null)
                {
                    var user = await authService.GetByEmailAsync(userId);
                    if (user != null)
                    {
                        var claims = new List<Claim> {
                        new(ClaimTypes.NameIdentifier, user.Id),
                        new(ClaimTypes.Role, user.Role)
                    };
                        var identity = new ClaimsIdentity(claims, "jwt");
                        context.User = new ClaimsPrincipal(identity);
                    }
                }
            }

            await _next(context);
        }
    }
}
