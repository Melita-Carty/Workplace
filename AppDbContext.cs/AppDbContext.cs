using BasicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees {get; set;}
    public DbSet<Department> Departments {get; set;}

    public DbSet<Project> Projects {get; set;}
}

