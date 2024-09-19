using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Entities
{
    public class AppDbContext : DbContext
    {
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<UserAccount> UserAccounts{ get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);}
    }
}