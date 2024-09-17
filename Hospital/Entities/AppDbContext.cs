using Microsoft.EntityFrameworkCore;
using tryingSystem.Models;

namespace tryingSystem.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<UserAccount> UserAccounts{ get; set; }
        public DbSet<TodoItemModel> TodoItems{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasMany(u => u.TodoItems)
            .WithOne(t => t.UserAccount).HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }

    }
}