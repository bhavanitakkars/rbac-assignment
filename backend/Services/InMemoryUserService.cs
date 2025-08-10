
using System.Collections.Concurrent;

// Simple in-memory user store for demo
namespace RbacBackend
{
    public class User
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = ""; 
        public string Role { get; set; } = "";
    }

    public class InMemoryUserService : IUserService
    {
        private readonly ConcurrentDictionary<string, User> _users = new();

        public InMemoryUserService()
        {
            _users.TryAdd("admin", new User { Username = "admin", Password = "admin@789", Role = "Admin" });
            _users.TryAdd("editor", new User { Username = "editor", Password = "editor@789", Role = "Editor" });
            _users.TryAdd("viewer", new User { Username = "viewer", Password = "viewer@789", Role = "Viewer" });
        }

        public User? ValidateCredentials(string username, string password)
        {
            if (_users.TryGetValue(username, out var user) && user.Password == password)
                return user;
            return null;
        }
        public bool AddUser(User user)
        {
            // TryAdd returns false if user with same username exists
            return _users.TryAdd(user.Username, user);
        }
        public bool UpdateUser(User user)
        {
            if (!_users.ContainsKey(user.Username))
                return false;

            _users[user.Username] = user;  // Replace the existing user object with the updated one
            return true;
        }

        public bool DeleteUser(string username)
        {
            return _users.TryRemove(username, out _);
        }
        public IEnumerable<User> GetAll() => _users.Values.ToArray();
    }
}
