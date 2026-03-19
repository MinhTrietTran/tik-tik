namespace tiktik.Domain.Entities;

public class TodoTask
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;

    public DateTime CreateAt {get; set;}

    public bool IsDone {get; set;}

    public int UserId {get; set;}
}