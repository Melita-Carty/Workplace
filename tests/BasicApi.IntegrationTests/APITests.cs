using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using BasicApi;
using BasicApi.Models;
using System.Data.Common;
using System.Text.Json;

namespace BasicApi.IntegrationTests;

public class EmployeeEndpointTests
{

    [Test]
    public async Task GetEmployees_ReturnsOk()
        /*
        Assert that the endpoint returns the right data
        */
    {
        // Arrange
        await using var application = new CustomWebApplicationFactory();
        var client = application.CreateClient();

        // Seed data
        using (var scope = application.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Employees.Add(new Employee {
                FirstName = "Alice",
                LastName = "Paul",
                Department = new Department {
                    Name = "Department A"
                }
            });
            db.SaveChanges();
        }

        // Act
        var response = await client.GetAsync("/api/employee");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
        Assert.That(employees!.Count, Is.EqualTo(1));
        Assert.That(employees[0].FirstName, Is.EqualTo("Alice"));


    }

}
