using Microsoft.EntityFrameworkCore;
using MigrationsTools.WebApi.Models;

namespace MigrationsTools.WebApi.Data;

public class SchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=MigrationTools;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=true;");
    }
}