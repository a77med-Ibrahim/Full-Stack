using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using tryingSystem.Models;

namespace tryingSystem.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<UserAccount> UserAccounts{ get; set; }
        public DbSet<TaskInit> Tasks {get;set;}

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Existing seed data
    modelBuilder.Entity<UserAccount>().HasData(
        new UserAccount{
            Id = 1,
            FirstName = "Admin", 
            LastName = "Manager",
            UserName = "admin",
            Email = "admin11@gmail.com",
            Password = "A123456789",
            Role = "Manager"
        }
    );

    modelBuilder.Entity<TaskInit>()
        .HasOne(t => t.AssignedToNurse)
        .WithMany()
        .HasForeignKey(t => t.AssignedToNurseId)
        .OnDelete(DeleteBehavior.Restrict);  

    modelBuilder.Entity<TaskInit>()
        .HasOne(t => t.AssignedByDoctor)
        .WithMany()
        .HasForeignKey(t => t.AssignedByDoctorId)
        .OnDelete(DeleteBehavior.Cascade);  
}
    }
}