using Microsoft.EntityFrameworkCore;
using TodoApi.TodoApp.Domain.Entities;

namespace TodoApi.TodoApp.Infrastructure.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
            Console.WriteLine("application db context constructor");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>()
                .HasOne<User>()
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);  
        }
    }
}