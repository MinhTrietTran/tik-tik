using tiktik.Domain.Entities;
using tiktik.Application.Interfaces;

namespace tiktik.Application.Services;

public class TodoService
{
    private readonly ITodoRepository _repo;
    public TodoService(ITodoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TodoTask>> GetTasksByUserIdAsync(int userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }

    public async Task<TodoTask> CreateAsync(string title, int userId)
    {
        //1. Validate input
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");
        
        //2. Create entity
        var task = new TodoTask
        {
            Title = title,
            CreateAt = DateTime.UtcNow,
            IsDone = false,
            UserId = userId
        };

        //3. Call repository
        await _repo.AddAsync(task);
        return task;
    }

    public async Task MarkAsDoneAsync(int taskId)
    {
        //1. Get task
        var task = await _repo.GetByIdAsync(taskId);
        //2. if null throw handle exception
        if (task == null)
            throw new KeyNotFoundException("Task not found.");
        //3. Update IsDone
        task.IsDone = true;


        //3. Call repository
        await _repo.UpdateAsync(task);
    }
}