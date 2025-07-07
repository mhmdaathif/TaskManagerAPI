using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) return null;

            if (!PasswordHasher.Verify(password, user.Password))
                return null;

            user.Token = Guid.NewGuid().ToString();
            await _userRepo.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByTokenAsync(string token) =>
            await _userRepo.GetByTokenAsync(token);
    }
}
