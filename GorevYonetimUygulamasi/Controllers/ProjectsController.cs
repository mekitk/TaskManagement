using Görev_Yönetim_Uygulaması.Models;
using Görev_Yönetim_Uygulaması.Services;
using GorevYonetimUygulamasi.Dto;
using GorevYonetimUygulamasi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Görev_Yönetim_Uygulaması.Controllers
{   
    //YetkiKontrolleri
    [ApiController]
    [Route("projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;

        public ProjectsController(ProjectService service, TaskService taskService)
        {
            _projectService = service;
            _taskService = taskService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();

            var result = new List<ProjectGetDto>();

            foreach (var p in projects)
            {
                var taskCount = await _taskService.GetTaskCountByProjectId(p.Id);

                result.Add(new ProjectGetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    MemberIds = p.MemberIds,
                    Status = p.Status,
                    TasksCount = taskCount
                });
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ownerId == null)
                return Unauthorized("Token geçerli değil. Kullanıcı ID'si alınamadı.");

            var project = new Project
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                MemberIds = dto.MemberIds,
                Status = dto.Status,
                OwnerId = ownerId
            };

            await _projectService.CreateAsync(project);
            return Ok(project);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateProject(string id, [FromBody] ProjectUpdateDto dto)
        {
            var existing = await _projectService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Sadece güncellenebilir alanlar
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.MemberIds = dto.MemberIds;

            await _projectService.UpdateAsync(id, existing);
            return Ok(existing);
        }
    }
}
