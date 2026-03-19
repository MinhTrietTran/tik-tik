using tiktik.Domain.Entities;
namespace tiktik.Application.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoTask>> GetByUserIdAsync(int userId);
    Task<TodoTask?> GetByIdAsync(int taskId);
    Task AddAsync(TodoTask task);
    Task UpdateAsync(TodoTask task);
    //Task DeleteAsync(int taskId);
}