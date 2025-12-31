// using BasicApi.Data;
using BasicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetDepartmentsAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department> GetDepartmentAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<DepartmentEmployeeDto?> GetDepartmentWithEmployeesAsync(int id)
    {
        return await _context.Departments
            .Include(d => d.Employees)
            .Select(d => new DepartmentEmployeeDto
            {
                Id = d.Id,
                Name = d.Name,
                Employees = d.Employees!.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList()
            })
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsWithProjects(string? sortBy)
    {
        IQueryable<Department> departments = _context.Departments.Include(d => d.Projects);

        if (sortBy == "projectCount")
        {
            departments = departments.OrderByDescending(d => d.Projects.Count);
        }

        return await departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            ProjectCount = d.Projects.Count
        }).ToListAsync();

    }
}
