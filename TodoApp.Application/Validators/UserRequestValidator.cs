using FluentValidation;
using TodoApi.TodoApp.Domain.DTOs;

namespace TodoApi.TodoApp.Application.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
        }
    }
}