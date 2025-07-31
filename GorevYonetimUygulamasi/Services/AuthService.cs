using Görev_Yönetim_Uygulaması.Models;
using MongoDB.Driver;


namespace Görev_Yönetim_Uygulaması.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _users;

        public AuthService(IMongoDatabase db)
        {
            _users = db.GetCollection<User>("Users");
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _users.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<User> RegisterAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
    }
}
