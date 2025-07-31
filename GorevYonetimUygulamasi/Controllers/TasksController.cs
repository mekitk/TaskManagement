using global::Görev_Yönetim_Uygulaması.Services;
using Görev_Yönetim_Uygulaması.Models;
using GorevYonetimUygulamasi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GorevYonetimUygulamasi.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Authorize]  // Genel olarak kimlik doğrulama zorunlu
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly TaskLogService _taskLogService;

        public TasksController(TaskService taskService, TaskLogService taskLogService)
        {
            _taskService = taskService;
            _taskLogService = taskLogService;
        }

        // Kullanıcının atanmış görevlerini getirir
        [HttpGet]
        public async Task<IActionResult> GetMyTasks()
        
       
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _taskService.GetAssignedToUserAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("gettasks")]
        public async Task<IActionResult> GetTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            if (userRoles.Contains("Admin"))
            {
                // Admin ise tüm görevleri getir
                var allTasks = await _taskService.GetAllTasksAsync();
                return Ok(allTasks);
            }
            else
            {
                // Admin değilse sadece kendi görevlerini getir
                var tasks = await _taskService.GetAssignedToUserAsync(userId);
                return Ok(tasks);
            }
        }

        // ID ile görev getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (task.AssignedTo != userId)
                return Forbid();

            return Ok(task);
        }

        // Yeni görev oluşturma - sadece Manager ve Admin
        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            task.CreatedBy = userId;
            task.CreatedAt = DateTime.UtcNow;
            
            // Opsiyonel default değerler:
            if (task.Status == 0)
                task.Status = TaskStatusEnum.Beklemede;

            try
            {
                await _taskService.CreateAsync(task);
                return Ok(task);
            }
            catch (Exception ex)
            {
                // Burada loglama yapılabilir
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        // Görev güncelleme - sadece Manager ve Admin
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateTask(string id, TaskItem updatedTask)
        {
            var existingTask = await _taskService.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound();

            bool statusChanged = existingTask.Status != updatedTask.Status;

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.Priority = updatedTask.Priority;
            existingTask.AssignedTo = updatedTask.AssignedTo;

            await _taskService.UpdateAsync(existingTask);

            if (statusChanged)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var log = new TaskLog
                {
                    TaskId = existingTask.Id,
                    Status = updatedTask.Status,
                    ChangedBy = userId,
                    ChangedAt = DateTime.UtcNow
                };
                await _taskLogService.AddLogAsync(log);
            }

            return Ok(existingTask);
        }

        // Görev silme - sadece Admin
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var existingTask = await _taskService.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound();

            await _taskService.DeleteAsync(id);
            await _taskLogService.DeleteLogsByTaskIdAsync(id);

            return NoContent();
        }
    }
}
