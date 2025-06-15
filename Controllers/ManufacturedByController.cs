// Controllers/ManufacturedByController.cs
using Microsoft.AspNetCore.Mvc;
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;
using System.Linq;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturedByController : ControllerBase
    {
        private readonly FleetManagementContext _context;

        public ManufacturedByController(FleetManagementContext context)
        {
            _context = context;
        }

        // GET: api/ManufacturedBy
        [HttpGet]
        public IActionResult GetAllManufacturedBy()
        {
            var manufacturers = _context.ManufacturedBy.ToList();
            return Ok(manufacturers);
        }

        // GET: api/ManufacturedBy/{id}
        [HttpGet("{id}")]
        public IActionResult GetManufacturedBy(int id)
        {
            var manufacturer = _context.ManufacturedBy.Find(id);
            if (manufacturer == null)
            {
                return NotFound(new { message = "Manufacturer not found" });
            }
            return Ok(manufacturer);
        }

        // POST: api/ManufacturedBy
        [HttpPost]
        public IActionResult AddManufacturedBy([FromBody] ManufacturedBy manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ManufacturedBy.Add(manufacturer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetManufacturedBy), new { id = manufacturer.Id }, manufacturer);
        }

        // PUT: api/ManufacturedBy/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateManufacturedBy(int id, [FromBody] ManufacturedBy updatedManufacturer)
        {
            var manufacturer = _context.ManufacturedBy.Find(id);
            if (manufacturer == null)
            {
                return NotFound(new { message = "Manufacturer not found" });
            }

            // Validate uniqueness of the name
            if (_context.ManufacturedBy.Any(m => m.Name == updatedManufacturer.Name && m.Id != id))
            {
                return BadRequest(new { message = "A manufacturer with this name already exists." });
            }

            manufacturer.Name = updatedManufacturer.Name;
            manufacturer.Description = updatedManufacturer.Description;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/ManufacturedBy/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturedBy(int id)
        {
            var manufacturer = _context.ManufacturedBy.Find(id);
            if (manufacturer == null)
            {
                return NotFound(new { message = "Manufacturer not found" });
            }

            if (manufacturer.IsLinkedToProduct)
            {
                return BadRequest(new { message = "Cannot delete a manufacturer linked to a product" });
            }

            _context.ManufacturedBy.Remove(manufacturer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}