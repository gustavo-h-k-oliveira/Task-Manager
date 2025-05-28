using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;

        public TasksController(TaskManagerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskManager.Domain.Entities.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult<TaskManager.Domain.Entities.Task>> CreateTask(TaskManager.Domain.Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }
    }
}
