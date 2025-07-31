using Görev_Yönetim_Uygulaması.Models;
using MongoDB.Driver;

namespace Görev_Yönetim_Uygulaması.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskService(IMongoDatabase database)
        {
            _tasks = database.GetCollection<TaskItem>("Tasks");
        }

        // Bir projeye ait tüm görevleri getir
        public async Task<List<TaskItem>> GetByProjectIdAsync(string projectId)
        {
            return await _tasks.Find(t => t.ProjectId == projectId).ToListAsync();
        }

        // ID'ye göre görev getir
        public async Task<TaskItem> GetByIdAsync(string taskId)
        {
            return await _tasks.Find(t => t.Id == taskId).FirstOrDefaultAsync();
        }

        // Yeni görev oluştur
        public async Task CreateAsync(TaskItem task)
        {
            await _tasks.InsertOneAsync(task);
        }

        // Görevi güncelle (tüm objeyi replace eder)
        public async Task UpdateAsync(TaskItem updatedTask)
        {
            await _tasks.ReplaceOneAsync(t => t.Id == updatedTask.Id, updatedTask);
        }

        // Görevi sil
        public async Task DeleteAsync(string id)
        {
            await _tasks.DeleteOneAsync(t => t.Id == id);
        }

        // Kullanıcıya atanmış görevleri getir
        public async Task<List<TaskItem>> GetAssignedToUserAsync(string userId)
        {
            return await _tasks.Find(t => t.AssignedTo == userId).ToListAsync();
        }

        // Kullanıcının projelerindeki görevleri getir (isteğe bağlı)
        public async Task<List<TaskItem>> GetTasksByUserAsync(string userId)
        {
            // Burada "AssignedTo" ya da "CreatedBy" gibi alanlar kullanılarak filtre yapılabilir
            return await _tasks.Find(t => t.AssignedTo == userId).ToListAsync();
        }

        public async Task<int> GetTaskCountByProjectId(string projectId)
        {
            return (int)await _tasks.CountDocumentsAsync(t => t.ProjectId == projectId);
        }
        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            // Filtre vermeden tüm dökümanları çek
            return await _tasks.Find(_ => true).ToListAsync();
        }
    }
}
