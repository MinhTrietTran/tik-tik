
using tiktik.Application.Interfaces;
using tiktik.Domain.Entities;

public class InmemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoTask> _tasks = new List<TodoTask>();
    public Task<List<TodoTask>> GetByUserIdAsync(int userId)
    {
        var result = _tasks.Where(t => t.UserId == userId).ToList();
        return Task.FromResult(result);
    }
    public Task<TodoTask?> GetByIdAsync(int taskId)
    {
        return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == taskId));
    }
    public Task AddAsync(TodoTask task)
    {
        _tasks.Add(task);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(TodoTask task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);
        if(index >= 0)
        {
            _tasks[index] = task;
        }
        return Task.CompletedTask;
    }

}