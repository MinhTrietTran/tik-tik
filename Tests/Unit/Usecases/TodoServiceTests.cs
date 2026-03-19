using Xunit;
using tiktik.Application.Services;

[TestClass]
public class TodoServiceTests
{
    private InmemoryTodoRepository _repo = null!;
    private TodoService _service = null!;
    private int _userId = 1;

    [TestInitialize]
    public void Setup()
    {
        _repo = new InmemoryTodoRepository();
        _service = new TodoService(_repo);
        _userId = 1;
    }

    [TestMethod]
    public async Task CreateAsync_ShouldAddTask()
    {
        await _service.CreateAsync("Test Task", _userId);

        var tasks = await _service.GetTasksByUserIdAsync(_userId);
        Xunit.Assert.Single(tasks);
        Xunit.Assert.Equal("Test Task", tasks[0].Title);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrow_WhenTitleEmpty()
    {
        await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("",_userId)
        );
    }

    [TestMethod]
    public async Task GetByUserIdAsync_ShouldReturnCorrectTasks()
    {
        var user2 = 2;
        await _service.CreateAsync("Task 1", _userId);
        await _service.CreateAsync("Task 2", user2);
        var result = await _service.GetTasksByUserIdAsync(_userId);
        Xunit.Assert.Single(result);
        Xunit.Assert.Equal("Task 1", result[0].Title);

    }

    [TestMethod]
    public async Task MarkAsDoneAsync_ShouldSetIsDoneTrue()
    {
        // await _service.CreateAsync("Task to complete", _userId);
        // var tasks = await _service.GetTasksByUserIdAsync(_userId);
        var task = await _service.CreateAsync("Task to complete", _userId);
        await _service.MarkAsDoneAsync(task.Id);
        var updatedTask = await _repo.GetByIdAsync(task.Id);
        Xunit.Assert.NotNull(updatedTask);
        Xunit.Assert.True(updatedTask!.IsDone);
    }

}
