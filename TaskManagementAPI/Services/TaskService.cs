using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;

        public TaskService(ITaskRepository taskRepo) => _taskRepo = taskRepo;

        public async Task<List<TaskItem>> GetTasksAsync(int userId) =>
            await _taskRepo.GetTasksByUserIdAsync(userId);

        public async Task<TaskItem?> GetTaskAsync(int id, int userId) =>
            await _taskRepo.GetTaskByIdAsync(id, userId);

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            await _taskRepo.AddTaskAsync(task);
            await _taskRepo.SaveChangesAsync();
            return task;
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _taskRepo.UpdateTask(task);
            await _taskRepo.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id, int userId)
        {
            var task = await _taskRepo.GetTaskByIdAsync(id, userId);
            if (task != null)
            {
                _taskRepo.DeleteTask(task);
                await _taskRepo.SaveChangesAsync();
            }
        }
    }
}
