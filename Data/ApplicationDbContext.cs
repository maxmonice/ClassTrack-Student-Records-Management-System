using ClassTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassTrack.Data;

// The bridge between our C# classes and the SQL Server database.
public class ApplicationDbContext : DbContext
{
    // Options (provider + connection string) are supplied by dependency injection in Program.cs.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Each DbSet becomes a table. By convention, "Students" is the table name
    // and Student.Id is the primary key (identity column). No extra configuration needed.
    public DbSet<Student> Students { get; set; }
}
