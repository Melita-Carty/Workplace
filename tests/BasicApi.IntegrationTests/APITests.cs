using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using BasicApi;
using BasicApi.Models;

namespace BasicApi.IntegrationTests;

public class EmployeeEndpointTests
{
    [Test]
    public async Task GetEmployees_ReturnsOk()
    {
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

        var response = await client.GetAsync("/api/employee");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

}
