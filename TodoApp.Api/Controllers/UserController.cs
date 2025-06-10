using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.TodoApp.Application.Users.Commands;
using TodoApi.TodoApp.Application.Users.Queries;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Get all users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        if (users == null || !users.Any())
        {
            return NotFound(new { message = "No users found." });
        }

        return Ok(users);
    }

    // Get user by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(user);
    }

    // Get user by username
    [HttpGet("find/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _mediator.Send(new GetUserByUsernameQuery(username));

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(user);
    }

    // Add a new user
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {
        var response = await _mediator.Send(new CreateUserCommand(request));

        return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
    }

    // Update username
    [HttpPut("{id}/update-username")]
    public async Task<IActionResult> UpdateUsername(int id, [FromBody] string newUsername)
    {
        var response = await _mediator.Send(new UpdateUserCommand(id, newUsername));

        if (response == null)
        {
            return NotFound(new { message = "User not found or username already taken." });
        }

        return Ok(response);
    }

    // Delete user
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));

        if (!result)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new { message = "User deleted successfully." });
    }
}
