namespace TodoApi.TodoApp.Domain.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}