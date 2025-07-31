using Görev_Yönetim_Uygulaması.Models;
using MongoDB.Driver;

namespace Görev_Yönetim_Uygulaması.Services
{
    public class TaskLogService
    {
        private readonly IMongoCollection<TaskLog> _logs;

        public TaskLogService(IMongoDatabase database)
        {
            _logs = database.GetCollection<TaskLog>("TaskLogs");
        }

        public async Task<List<TaskLog>> GetLogsByTaskIdAsync(string taskId)
        {
            return await _logs.Find(log => log.TaskId == taskId)
                              .SortByDescending(log => log.ChangedAt)
                              .ToListAsync();
        }

        public async Task AddLogAsync(TaskLog log)
        {
            log.ChangedAt = DateTime.UtcNow;
            await _logs.InsertOneAsync(log);
        }

        public async Task DeleteLogsByTaskIdAsync(string taskId)
        {
            await _logs.DeleteManyAsync(log => log.TaskId == taskId);
        }
    }
}
