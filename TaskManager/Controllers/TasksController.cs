using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.DTOs;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskItemService _service;

        public TaskItemsController(TaskItemService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById(int id)
        {
            var taskItem = await _service.GetTaskByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            var taskDto = new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description ?? string.Empty,
                IsCompleted = taskItem.IsCompleted
            };

            return Ok(taskDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            var taskDtos = tasks.Select(task => new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description ?? string.Empty,
                IsCompleted = task.IsCompleted
            });
            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask(CreateTaskItemDto createDto)

        {
            var taskItem = new TaskItem
            {
                Title = createDto.Title,
                Description = createDto.Description,
                IsCompleted = false
            };

            await _service.CreateTaskAsync(taskItem);

            var taskDto = new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = taskItem.Id }, taskDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, UpdateTaskItemDto updateDto)
        {
            var existingTask = await _service.GetTaskByIdAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = updateDto.Title;
            existingTask.Description = updateDto.Description;
            existingTask.IsCompleted = updateDto.IsCompleted;

            await _service.UpdateTaskAsync(existingTask);

            return NoContent(); // 204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _service.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
