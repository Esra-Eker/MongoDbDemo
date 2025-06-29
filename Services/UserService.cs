using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbDemo.Models;

namespace MongoDbDemo.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>(settings.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<User> GetAsync(string id) =>
            await _users.Find(k => k.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User user) =>
            await _users.ReplaceOneAsync(k => k.Id == id, user);

        public async Task DeleteAsync(string id) =>
            await _users.DeleteOneAsync(k => k.Id == id);
    }

}
