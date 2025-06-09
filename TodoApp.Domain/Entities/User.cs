namespace TodoApi.TodoApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        //(one-to-many relationship)
        public List<TodoItem> Todos { get; set; } = new List<TodoItem>();
    }
}