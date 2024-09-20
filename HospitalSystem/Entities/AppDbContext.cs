using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Entities
{
     public class Message
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public int DoctorId { get; set; }
            public UserAccount Doctor { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    public class AppDbContext : DbContext
    {
       
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        
        public DbSet<UserAccount> UserAccounts{ get; set; }
        public DbSet<Message> Messages { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<UserAccount>().HasData(new UserAccount
    {
        Id = 1231212312,
        FirstName = "Admin",
        LastName = "Manager",
        Email = "manager@hospital.com",
        UserName = "manager",
        Password = "Manager@123",
        Role = "Manager"
    });}
    }
}