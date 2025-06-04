using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services
{
    public class TaskItemService
    {
        private readonly ITaskItemRepository _repository;

        public TaskItemService(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem)
        {
            return await _repository.AddAsync(taskItem);
        }

        public async Task UpdateTaskAsync(TaskItem taskItem)
        {
            await _repository.UpdateAsync(taskItem);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
