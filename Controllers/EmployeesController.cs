using Microsoft.AspNetCore.Mvc;
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;
using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly UserManager<Employee> _context;

    public EmployeeController(UserManager<Employee> context)
    {
        _context = context;
    }

    // GET: api/employee
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _context.Users.ToListAsync();
        return Ok(employees);
    }

    // GET: api/employee/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(string id)
    {
        var employee = await _context.FindByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // POST: api/employee
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Invalid employee data.");
        }

        // Hash the password before saving
        //employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);

        //_context.Employees.Add(employee);
        //_context.SaveChanges();

        var user = await _context.FindByNameAsync(employee.UserName);
        if (user != null) 
            return BadRequest($"The username {employee.UserName} has been already taken");

        var identityResult = await _context.CreateAsync(employee, employee.PasswordHash);

        if (!identityResult.Succeeded)
            return BadRequest("Registration unsuccessful");

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // PUT: api/employee/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] Employee updatedEmployee)
    {
        var existingEmployee = await _context.FindByIdAsync(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        // Update only the fields provided in the request
        if (updatedEmployee.FirstName != null) existingEmployee.FirstName = updatedEmployee.FirstName;
        if (updatedEmployee.LastName != null) existingEmployee.LastName = updatedEmployee.LastName;
        if (updatedEmployee.UserName != null) existingEmployee.UserName = updatedEmployee.UserName;
        if (updatedEmployee.PasswordHash != null) existingEmployee.PasswordHash = updatedEmployee.PasswordHash;
        if (updatedEmployee.EmployeeCode != null) existingEmployee.EmployeeCode = updatedEmployee.EmployeeCode;
        if (updatedEmployee.EmployeeType != null) existingEmployee.EmployeeType = updatedEmployee.EmployeeType;
        if (updatedEmployee.IsActive != null) existingEmployee.IsActive = updatedEmployee.IsActive;

        var identityResult = await _context.UpdateAsync(existingEmployee);

        return NoContent(); // 204 No Content for successful updates
    }

    // DELETE: api/employee/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        var employee = await _context.FindByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        await _context.DeleteAsync(employee);
        //_context.SaveChanges();

        return NoContent();
    }
}