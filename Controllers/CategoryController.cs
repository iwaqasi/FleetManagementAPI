// CategoryController.cs
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly FleetManagementContext _context;
        private readonly IMapper _mapper; // Add IMapper as a dependency

        public CategoryController(FleetManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; // Inject IMapper
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            // Use AutoMapper to map entities to DTOs
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            // Use AutoMapper to map the entity to a DTO
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the CategoryCode is unique
            if (await _context.Categories.AnyAsync(c => c.CategoryCode == request.CategoryCode))
            {
                return Conflict(new { message = "CategoryCode must be unique" });
            }

            // Validate ParentCategoryId if provided
            if (request.ParentCategoryId.HasValue)
            {
                var parentCategory = await _context.Categories.FindAsync(request.ParentCategoryId.Value);
                if (parentCategory == null)
                {
                    return BadRequest(new { message = "ParentCategoryId does not exist" });
                }
            }

            // Use AutoMapper to map the request to a Category entity
            var category = _mapper.Map<Category>(request);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Map the created entity back to a DTO for the response
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDto);
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            // Check if the CategoryCode is unique (excluding the current category)
            if (await _context.Categories.AnyAsync(c => c.CategoryCode == request.CategoryCode && c.Id != id))
            {
                return Conflict(new { message = "CategoryCode must be unique" });
            }
            // Use AutoMapper to map the request to the existing entity
            _mapper.Map(request, category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            // Prevent deletion if the category has sub-categories
            if (await _context.Categories.AnyAsync(c => c.ParentCategoryId == id))
            {
                return BadRequest(new { message = "Cannot delete a category with sub-categories" });
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // Request model for creating/updating categories
    public class CategoryRequest
    {
        [Required]
        public string CategoryCode { get; set; }
        [Required]
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; } // Nullable for top-level categories
    }
}