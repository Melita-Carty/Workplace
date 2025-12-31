using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BasicApi.Models;

namespace Kata12BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _service.GetDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _service.GetDepartmentAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);

        }
        [HttpGet("{id}/employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartment(int id)
        {

            var department = await _service.GetDepartmentWithEmployeesAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Department>>> GetProjectsByDepartment(string? sortBy = null)
        {
            var departments = await _service.GetDepartmentsWithProjects(sortBy);

            return Ok(departments);

        }
    }

}