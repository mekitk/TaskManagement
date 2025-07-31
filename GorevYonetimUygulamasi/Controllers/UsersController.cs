using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using GorevYonetimUygulamasi.Services;

namespace GorevYonetimUygulamasi.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]  // Gerekirse yetkiyi buraya koy
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            var result = users.Select(u => new {
                Id = u.Id,
                Name = u.Username,
                Role = u.Role,
                Email = u.Email
            });

            return Ok(result);
        }
    }
}