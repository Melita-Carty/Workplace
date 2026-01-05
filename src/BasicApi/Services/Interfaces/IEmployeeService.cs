
using BasicApi.Models;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetEmployeesAsync();
   

}
