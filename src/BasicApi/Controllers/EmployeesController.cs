using BasicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;
    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _context.Employees.Include(employees => employees.Department).Select(e => new EmployeeDto{
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToListAsync();
        return Ok(employees);
    }

    
}
}