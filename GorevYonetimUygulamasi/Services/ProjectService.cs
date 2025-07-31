using Görev_Yönetim_Uygulaması.Models;
using MongoDB.Driver;

namespace Görev_Yönetim_Uygulaması.Services
{

    public class ProjectService
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectService(IMongoDatabase database)
        {
            _projects = database.GetCollection<Project>("Projects");
        }

        public async Task<List<Project>> GetAllByUserAsync(string userId)
        {
            var filter = Builders<Project>.Filter.Or(
                Builders<Project>.Filter.Eq(p => p.OwnerId, userId),
                Builders<Project>.Filter.AnyEq(p => p.MemberIds, userId)
            );

            return await _projects.Find(filter).ToListAsync();
        }

        public async Task<Project> GetByIdAsync(string id)
        {
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task UpdateAsync(string id, Project updatedProject)
        {
            await _projects.ReplaceOneAsync(p => p.Id == id, updatedProject);
        }

        public async Task DeleteAsync(string id)
        {
            await _projects.DeleteOneAsync(p => p.Id == id);
        }
        public async Task<List<Project>> GetAllAsync()
        {
            return await _projects.Find(_ => true).ToListAsync();
        }
    }

}
