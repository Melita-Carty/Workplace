using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Kata12BasicAPI.Controllers;
using BasicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Tests;

[TestFixture]
public class DepartmentControllerTests

{
    // private Mock<AppDbContext> _contextMock;
    private AppDbContext _context;
    private DepartmentController _controller;


    [SetUp]
    public void SetUp()
    {
        // _contextMock = new Mock<AppDbContext>();
        // _controller = new DepartmentController(_contextMock.Object);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new AppDbContext(options);

        _context.Departments.AddRange(
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "Finance" }
        );

        _context.SaveChanges();
        _controller = new DepartmentController(_context);
    }

    [Test]
    public async Task GetDepartments_WhenCalled_ReturnsDepartments()
    {
        // Arrange
        // var expected = 

        // Act
        // var result = _controller.GetDepartments() as OkObjectResult;

        // Assert

        // Act 
        var result = await _controller.GetDepartments();
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        var departments = okResult.Value as IEnumerable<Department>;
        Assert.That(departments.Count(), Is.EqualTo(2));
    }
}