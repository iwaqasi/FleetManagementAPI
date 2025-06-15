using Microsoft.AspNetCore.Mvc;
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly FleetManagementContext _context;

    public SupplierController(FleetManagementContext context)
    {
        _context = context;
    }

    // GET: api/supplier
    [HttpGet]
    public IActionResult GetAllSuppliers()
    {
        var suppliers = _context.Suppliers.ToList();
        return Ok(suppliers);
    }

    // GET: api/supplier/{id}
    [HttpGet("{id}")]
    public IActionResult GetSupplier(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier == null)
        {
            return NotFound(new { message = "Supplier not found" });
        }
        return Ok(supplier);
    }

    // POST: api/supplier
    [HttpPost]
    public IActionResult CreateSupplier([FromBody] Supplier supplier)
    {
        if (supplier == null)
        {
            return BadRequest(new { message = "Invalid supplier data" });
        }

        _context.Suppliers.Add(supplier);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
    }

    // PUT: api/supplier/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateSupplier(int id, [FromBody] Supplier updatedSupplier)
    {
        var existingSupplier = _context.Suppliers.Find(id);
        if (existingSupplier == null)
        {
            return NotFound(new { message = "Supplier not found" });
        }

        // Update only the fields provided in the request
        if (updatedSupplier.Code != null) existingSupplier.Code = updatedSupplier.Code;
        if (updatedSupplier.FirstName != null) existingSupplier.FirstName = updatedSupplier.FirstName;
        if (updatedSupplier.LastName != null) existingSupplier.LastName = updatedSupplier.LastName;
        if (updatedSupplier.CompanyName != null) existingSupplier.CompanyName = updatedSupplier.CompanyName;
        if (updatedSupplier.Email != null) existingSupplier.Email = updatedSupplier.Email;
        if (updatedSupplier.Phone1 != null) existingSupplier.Phone1 = updatedSupplier.Phone1;
        if (updatedSupplier.Phone2 != null) existingSupplier.Phone2 = updatedSupplier.Phone2;
        if (updatedSupplier.Address1 != null) existingSupplier.Address1 = updatedSupplier.Address1;
        if (updatedSupplier.Address2 != null) existingSupplier.Address2 = updatedSupplier.Address2;
        if (updatedSupplier.IsActive != null) existingSupplier.IsActive = updatedSupplier.IsActive;

        _context.SaveChanges();

        return NoContent(); // 204 No Content for successful updates
    }

    // DELETE: api/supplier/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteSupplier(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier == null)
        {
            return NotFound(new { message = "Supplier not found" });
        }

        _context.Suppliers.Remove(supplier);
        _context.SaveChanges();

        return NoContent();
    }

    // PATCH: api/supplier/{id}/status
    [HttpPatch("{id}/status")]
    public IActionResult UpdateSupplierStatus(int id, [FromBody] bool isActive)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier == null)
        {
            return NotFound(new { message = "Supplier not found" });
        }

        // Update the IsActive status
        supplier.IsActive = isActive;
        _context.SaveChanges();

        return NoContent(); // 204 No Content for successful updates
    }
}