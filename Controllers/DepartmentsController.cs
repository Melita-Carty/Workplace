using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BasicApi.Models;

namespace Kata12BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpGet("{id}/employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartment(int id)
        {
            var department = await _context.Departments
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
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }


    }



}