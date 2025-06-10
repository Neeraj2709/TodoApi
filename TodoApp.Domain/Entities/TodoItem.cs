namespace TodoApi.TodoApp.Domain.Entities;

public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Foreign key reference to User
    public int UserId { get; set; }

    // Navigation property (Ensure this exists!)
    public User User { get; set; }
}