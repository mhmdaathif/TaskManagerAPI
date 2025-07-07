using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task<TaskItem?> GetTaskByIdAsync(int id, int userId);
        Task AddTaskAsync(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
        Task SaveChangesAsync();
    }
}
