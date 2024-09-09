using Microsoft.EntityFrameworkCore;
using MinimalCrud.Students;

namespace MinimalCrud.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=localhost;Database=postgres;Username=postgres;Password=postgres";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}