

namespace RbacBackend
{
    public interface IUserService
    {
        User? ValidateCredentials(string username, string password);
        IEnumerable<User> GetAll();
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(string username);
    }
}
