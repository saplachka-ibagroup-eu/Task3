using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Task3.Model;

namespace Task3;

public class ApplicationContext : DbContext
{
    internal DbSet<Record> Records { get; set; }

    public ApplicationContext()
    {
        //  Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    // Configure your database connection string and other options
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
    }
}
