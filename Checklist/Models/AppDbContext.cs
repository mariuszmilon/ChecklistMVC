using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Checklist;Trusted_Connection=True";

        public DbSet<Assignment> Assignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
