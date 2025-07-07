using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        private User? GetCurrentUser() => HttpContext.Items["User"] as User;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = GetCurrentUser();
            if (user == null) return Unauthorized();

            var tasks = await _taskService.GetTasksAsync(user.Id);
            var dtoList = _mapper.Map<List<TaskDto>>(tasks);
            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto dto)
        {
            var user = GetCurrentUser();
            if (user == null) return Unauthorized();

            var task = _mapper.Map<TaskItem>(dto);
            task.UserId = user.Id;
            task.CreatedAt = DateTime.UtcNow;

            var created = await _taskService.CreateTaskAsync(task);
            var createdDto = _mapper.Map<TaskDto>(created);
            return Ok(createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskDto dto)
        {
            var user = GetCurrentUser();
            dto.UserId = user.Id;
            if (user == null) return Unauthorized();

            if (id != dto.Id) return BadRequest("ID mismatch");

            var existing = await _taskService.GetTaskAsync(id, user.Id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _taskService.UpdateTaskAsync(existing);

            var updatedDto = _mapper.Map<TaskDto>(existing);
            return Ok(updatedDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = GetCurrentUser();
            if (user == null) return Unauthorized();

            await _taskService.DeleteTaskAsync(id, user.Id);
            return NoContent();
        }
    }
}
