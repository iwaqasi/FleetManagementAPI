using Microsoft.AspNetCore.Mvc;
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;
using System.Linq;

namespace FleetManagementAPI.Controllers // No modifier before "namespace"
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly FleetManagementContext _context;

        public UnitOfMeasureController(FleetManagementContext context)
        {
            _context = context;
        }

        // GET: api/UnitOfMeasure
        [HttpGet]
        public IActionResult GetAllUnitsOfMeasure()
        {
            var units = _context.UnitsOfMeasure.ToList();
            return Ok(units);
        }

        // GET: api/UnitOfMeasure/{id}
        [HttpGet("{id}")]
        public IActionResult GetUnitOfMeasure(int id)
        {
            var unit = _context.UnitsOfMeasure.Find(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit of measure not found" });
            }
            return Ok(unit);
        }

        // POST: api/UnitOfMeasure
        [HttpPost]
        public IActionResult AddUnitOfMeasure([FromBody] UnitOfMeasure unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UnitsOfMeasure.Add(unit);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUnitOfMeasure), new { id = unit.Id }, unit);
        }

        // PUT: api/UnitOfMeasure/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUnitOfMeasure(int id, [FromBody] UnitOfMeasure updatedUnit)
        {
            var unit = _context.UnitsOfMeasure.Find(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit of measure not found" });
            }

            // Validate uniqueness of the name
            if (_context.UnitsOfMeasure.Any(u => u.Name == updatedUnit.Name && u.Id != id))
            {
                return BadRequest(new { message = "A unit of measure with this name already exists." });
            }

            unit.Name = updatedUnit.Name;
            unit.Description = updatedUnit.Description;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/UnitOfMeasure/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUnitOfMeasure(int id)
        {
            var unit = _context.UnitsOfMeasure.Find(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit of measure not found" });
            }

            if (unit.IsLinkedToProduct)
            {
                return BadRequest(new { message = "Cannot delete a unit of measure linked to a product" });
            }

            _context.UnitsOfMeasure.Remove(unit);
            _context.SaveChanges();

            return NoContent();
        }
    }
}