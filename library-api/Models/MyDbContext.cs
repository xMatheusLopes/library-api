using library_api.Models;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;User Id=root;Password=Matheushl1996*;Database=library", null);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }
}