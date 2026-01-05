using BasicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Services;

public class EmployeeService: IEmployeeService
{
    
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> GetEmployeesAsync()
    {
        
        return await _context.Employees.Include(employees => employees.Department).Select(e => new EmployeeDto{
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToListAsync();
    }

    
}