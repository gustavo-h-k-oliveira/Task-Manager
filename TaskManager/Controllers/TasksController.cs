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
        public async Task<ActionResult<TaskItemDto>> CreateTask([FromBody] CreateTaskItemDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] UpdateTaskItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _service.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            await _service.UpdateTaskAsync(task, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            await _service.DeleteTaskAsync(task);

            return NoContent();
        }
    }
}
