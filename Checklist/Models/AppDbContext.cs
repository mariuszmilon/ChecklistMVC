using Microsoft.EntityFrameworkCore;

namespace Checklist.Models
{
    public class AppDbContext : DbContext
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Checklist;Trusted_Connection=True";

        public DbSet<Assignment> Assignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
