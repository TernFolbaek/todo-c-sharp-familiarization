using Microsoft.EntityFrameworkCore;
using TODO.Models;

namespace TODO.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Primary key configurations
        modelBuilder.Entity<TodoItem>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<UserTodo>().HasKey(x => new { x.UserId, x.TodoId });

        // Configuring the many-to-many relationship via UserTodo
        modelBuilder.Entity<UserTodo>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTodos)
            .HasForeignKey(ut => ut.UserId);

        modelBuilder.Entity<UserTodo>()
            .HasOne(ut => ut.TodoItem) // Change Todo to TodoItem
            .WithMany(t => t.UserTodos) // Change t to ti (optional) for clarity
            .HasForeignKey(ut => ut.TodoId);
    }

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserTodo> UserTodos { get; set; } // Add this DbSet for the join table
}