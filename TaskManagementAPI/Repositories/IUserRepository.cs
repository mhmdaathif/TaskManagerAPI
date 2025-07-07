using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByTokenAsync(string token);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
