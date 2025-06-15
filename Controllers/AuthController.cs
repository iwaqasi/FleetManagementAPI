using AutoMapper;
using FleetManagementAPI.Data;
using FleetManagementAPI.DTOs;
using FleetManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Employee> _context;
    private readonly IMapper _mapper;

    public AuthController(UserManager<Employee> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] EmployeeCreationDto employee)
    {
        // Check if the user already exists
        var existingUser = await _context.FindByNameAsync(employee.UserName);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }
        // Set default values for the new user
        employee.IsActive = true;
        employee.LastLoginDate = DateTime.UtcNow;

        var entity =  _mapper.Map<Employee>(employee);

        // Create the user
        var result = await _context.CreateAsync(entity, employee.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Find the user by username
        var user = await _context.FindByNameAsync(request.UserName);

        if (user == null)
            return Unauthorized("Invalid Credentials");

        // Check if the user is active
        if (!user.IsActive)
            return Unauthorized(new { message = "You are not allowed to use the application. Please contact the administrator to activate or create an account." });

        // Check if the user exists and the password is valid
        var result = await _context.CheckPasswordAsync(user, request.Password);

        if(!result)
            return Unauthorized(new { message = "Invalid credentials" });

        //if (user == null || !BCrypt.Net.BCrypt.Verify(request.PasswordHash, user.PasswordHash))
        //{
        //    return Unauthorized(new { message = "Invalid credentials" });
        //}

        user.LastLoginDate = DateTime.UtcNow;
        var identityResult = await _context.UpdateAsync(user);

        if (!identityResult.Succeeded)
            return Unauthorized("Login details could not update"); //We will give a human readable message later

        //_context.SaveChanges();
        // Generate a JWT token
        var token = GenerateJwtToken(user);

        // Return the token and isActive status
        return Ok(new { token, isActive = user.IsActive });
    }

    private string GenerateJwtToken(Employee user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("G3J9xLq7zN2pX8vQwT5rYmPcZsA1oKfWtEhMjU4nB6dRG3J9xLq7zN2pX8vQwT5rYmPcZsA1oKfWtEhMjU4nB6dR"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.EmployeeType)
        };

        var token = new JwtSecurityToken(
            issuer: "CTS Garage Management",
            audience: "flutter-app",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string UserName,string Password);
