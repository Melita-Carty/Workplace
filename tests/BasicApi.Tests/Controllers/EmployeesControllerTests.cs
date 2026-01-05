// using NUnit.Framework;
// using Moq;
// using Microsoft.AspNetCore.Mvc;
// using Kata12BasicAPI.Controllers;
// using BasicApi.Models;

// namespace BasicApi.Tests;

// [TestFixture]
// public class EmployeeControllerTestsUnit

// {
//     [SetUp]
//     public void SetUp()
//     {
//     }

//     [Test]
//     public async Task GetEmployees_WhenCalled_ReturnsEmployees()
//     {
//         // Arrange
//         var mockService = new Mock<IEmployeeService>();

//         mockService.Setup(s => s.GetEmployeesAsync())
//             .ReturnsAsync(new List<Employee>
//             {
//                 new Employee { Id = 1, FirstName = "Bill", LastName= "Flower"},
//                  new Employee { Id = 1, FirstName = "Ben", LastName= "Pot"}
//             });

//         var controller = new EmployeeController(mockService.Object);

//         // Act
//         var result = await controller.GetEmployees();

//         // Assert
//         var ok = result.Result as OkObjectResult;
//         Assert.That(ok, Is.Not.Null);

//         var employees = ok.Value as IEnumerable<Employee>;
//         Assert.That(employees, Is.Not.Null);
//         Assert.That(employees.Count(), Is.EqualTo(2));
//     }}