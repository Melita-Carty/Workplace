using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Kata12BasicAPI.Controllers;
using BasicApi.Models;

namespace BasicApi.Tests;

[TestFixture]
public class DepartmentControllerTestsUnit

{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public async Task GetDepartments_WhenCalled_ReturnsDepartments()
    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();

        mockService.Setup(s => s.GetDepartmentsAsync())
            .ReturnsAsync(new List<Department>
            {
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" }
            });

        var controller = new DepartmentController(mockService.Object);

        // Act
        var result = await controller.GetDepartments();

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);

        var departments = ok.Value as IEnumerable<Department>;
        Assert.That(departments, Is.Not.Null);
        Assert.That(departments.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetDepartment_WhenCalledWithValidID_ReturnsDepartment()
    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();
        var departmentId = 1;

        mockService.Setup(s => s.GetDepartmentAsync(departmentId))
            .ReturnsAsync(
                new Department
                    { Id = departmentId, Name = "HR" }
            );

        var controller = new DepartmentController(mockService.Object);

        // Act
        var result = await controller.GetDepartment(departmentId);

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);

        var department = ok.Value as Department;
        Assert.That(department, Is.Not.Null);
    }

    [Test]
    public async Task GetDepartment_WhenCalledWithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();
        var departmentId = 1;
        var invalidDepartmentId = 10;

        mockService.Setup(s => s.GetDepartmentAsync(departmentId))
            .ReturnsAsync(
                new Department
                    { Id = departmentId, Name = "HR" }
            );

        var controller = new DepartmentController(mockService.Object);

        // Act
        var result = await controller.GetDepartment(invalidDepartmentId);

        // Assert
        var notFoundResult = result.Result as NotFoundResult;
        Assert.That(notFoundResult, Is.Not.Null);
    }

    [Test]
    public async Task GetEmployeesByDepartment_WhenCalledWithValdId_ReturnsDepartmentsWithEmployees()
    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();
        var controller = new DepartmentController(mockService.Object);

        var departmentId = 1;

        mockService.Setup(s => s.GetDepartmentWithEmployeesAsync(departmentId))
            .ReturnsAsync(
                new DepartmentEmployeeDto
                {
                    Id = 1,
                    Name = "Department A",
                    Employees = [
                        new EmployeeDto{
                            Id = 10,
                            FirstName = "Bob",
                            LastName = "Smith"
                        },
                        new EmployeeDto{
                            Id = 11,
                            FirstName = "Sarah",
                            LastName = "Jones"
                        }
                    ]
                }
            );

        // Act
        var result = await controller.GetEmployeesByDepartment(departmentId);

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);

        var department = ok.Value as DepartmentEmployeeDto;
        Assert.That(department, Is.Not.Null);

        Assert.That(department.Employees, Is.Not.Null);
        Assert.That(department.Employees.Count(), Is.EqualTo(2));

    }

    [Test]
    public async Task GetDepartmentProjects_WhenCalledWithoutSort_ReturnsDepartments()

    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();
        var controller = new DepartmentController(mockService.Object);

        mockService.Setup(s => s.GetDepartmentsWithProjects(null))
            .ReturnsAsync(new List<DepartmentDto>
            {
                new DepartmentDto
                {
                    Id = 1,
                    Name = "Department A",
                    ProjectCount = 10,
                },
                new DepartmentDto
                {
                    Id = 2,
                    Name = "Department B",
                    ProjectCount = 5,
                },
                new DepartmentDto
                {
                    Id = 3,
                    Name = "Department C",
                    ProjectCount = 14,
                }
            });

        // Act
        var result = await controller.GetProjectsByDepartment();

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);

        var department = ok.Value as List<DepartmentDto>;
        Assert.That(department, Is.Not.Null);

        Assert.That(department.Select(d => d.Id),
        Is.EqualTo(new[] { 1, 2, 3 }));

    }

    [Test]
    public async Task GetDepartmentProjects_WhenCalledWithSortBy_ReturnsDepartments()

    {
        // Arrange
        var mockService = new Mock<IDepartmentService>();
        var controller = new DepartmentController(mockService.Object);

        var sortBy = "projectCount";

        mockService.Setup(s => s.GetDepartmentsWithProjects(sortBy))
            .ReturnsAsync(new List<DepartmentDto>
            {
                new DepartmentDto
                {
                    Id = 3,
                    Name = "Department C",
                    ProjectCount = 14,
                },
                new DepartmentDto
                {
                    Id = 1,
                    Name = "Department A",
                    ProjectCount = 10,
                },
                new DepartmentDto
                {
                    Id = 2,
                    Name = "Department B",
                    ProjectCount = 5,
                }
            });

        // Act
        var result = await controller.GetProjectsByDepartment(sortBy);

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);

        var department = ok.Value as List<DepartmentDto>;
        Assert.That(department, Is.Not.Null);

        Assert.That(department.Select(d => d.Id),
        Is.EqualTo(new[] { 3, 1, 2 }));

    }

}