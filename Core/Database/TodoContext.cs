using Microsoft.EntityFrameworkCore;
using TutorialRestApi.Todo.App.Models;
using TutorialRestApi.User.App.Models;

namespace TutorialRestApi.Core.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        { }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        public DbSet<UserModel> Users { get; set; } = null!;
    }
}
