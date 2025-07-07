using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) => _context = context;

        public async Task<List<TaskItem>> GetTasksByUserIdAsync(int userId) =>
            await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

        public async Task<TaskItem?> GetTaskByIdAsync(int id, int userId) =>
            await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        public async Task AddTaskAsync(TaskItem task) => await _context.Tasks.AddAsync(task);

        public void UpdateTask(TaskItem task) => _context.Tasks.Update(task);

        public void DeleteTask(TaskItem task) => _context.Tasks.Remove(task);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
