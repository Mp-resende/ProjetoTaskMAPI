namespace TaskManagerAPI.Models;
public enum TaskStatus
{
    Pending,
    InProgress,
    Completed,
}

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly? DueDate { get; set; }
    public string? AssignedTo { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

}
