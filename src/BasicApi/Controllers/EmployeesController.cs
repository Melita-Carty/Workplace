using BasicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
{

     private readonly IEmployeeService _service;


    
    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees =  await _service.GetEmployeesAsync();
        return Ok(employees);
    }

    
}
}