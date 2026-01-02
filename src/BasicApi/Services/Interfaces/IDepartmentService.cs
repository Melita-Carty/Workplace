
using BasicApi.Models;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartmentsAsync();
    Task<Department?> GetDepartmentAsync(int id);
    Task<DepartmentEmployeeDto?> GetDepartmentWithEmployeesAsync(int id);
    Task<IEnumerable<DepartmentDto>> GetDepartmentsWithProjects(string? sortBy);

}
