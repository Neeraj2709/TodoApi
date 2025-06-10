using MediatR;
using TodoApi.TodoApp.Domain.DTOs;

public class UpdateUserCommand : IRequest<bool>
{
    public int Id { get; }
    public string NewUsername { get; }

    public UpdateUserCommand(int id, string newUsername)
    {
        Id = id;
        NewUsername = newUsername;
    }
}