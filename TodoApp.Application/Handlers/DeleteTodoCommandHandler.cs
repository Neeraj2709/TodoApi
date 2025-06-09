using MediatR;
using TodoApi.TodoApp.Application.Commands;
using TodoApi.TodoApp.Infrastructure.Data;

namespace TodoApi.TodoApp.Application.Handlers
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTodoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _context.TodoItems.FindAsync(request.Id);

            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}