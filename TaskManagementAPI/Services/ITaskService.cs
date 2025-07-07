using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetTasksAsync(int userId);
        Task<TaskItem?> GetTaskAsync(int id, int userId);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id, int userId);
    }
}
