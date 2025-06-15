// Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using FleetManagementAPI.Data;
using FleetManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FleetManagementAPI.DTOs;
using AutoMapper;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly FleetManagementContext _context;
        private readonly IMapper _mapper;

        public ProductController(FleetManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductSupplier)
                .Include(p => p.ProductManuName)
                .Include(p => p.ProductUoM)
                .ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductListDto>>(products);

            return Ok(productDtos);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductSupplier)
                .Include(p => p.ProductManuName)
                .Include(p => p.ProductUoM)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            // Validate uniqueness of ProductCode and ProductSKU
            if (_context.Products.Any(p => (p.ProductCode == updatedProduct.ProductCode || p.ProductSKU == updatedProduct.ProductSKU) && p.Id != id))
            {
                return BadRequest(new { message = "A product with this Product Code or SKU already exists." });
            }

            product.ProductCode = updatedProduct.ProductCode;
            product.ProductSKU = updatedProduct.ProductSKU;
            product.ProductCategoryId = updatedProduct.ProductCategoryId;
            product.ProductName = updatedProduct.ProductName;
            product.ProductBarcode = updatedProduct.ProductBarcode;
            product.ProductSupplierId = updatedProduct.ProductSupplierId;
            product.ProductManuNameId = updatedProduct.ProductManuNameId;
            product.ProductUoMId = updatedProduct.ProductUoMId;
            product.ProductWarranty = updatedProduct.ProductWarranty;
            product.ProductWarrantyStDate = updatedProduct.ProductWarrantyStDate;
            product.ProductWarrantyEndDate = updatedProduct.ProductWarrantyEndDate;
            product.ProductWarrantyDays = (int?)(updatedProduct.ProductWarrantyEndDate - updatedProduct.ProductWarrantyStDate).TotalDays;// updatedProduct.ProductWarrantyDays;
            product.ProductLocation = updatedProduct.ProductLocation;
            product.ProductOHQty = updatedProduct.ProductOHQty;
            product.ProductPriceUnit = updatedProduct.ProductPriceUnit;
            product.ProductLastPurchaseDate = updatedProduct.ProductLastPurchaseDate;
            product.ProductReceivedDate = updatedProduct.ProductReceivedDate;
            product.ProductLastApprovedDate = updatedProduct.ProductLastApprovedDate;
            //product.ProductCreatedBy = updatedProduct.ProductCreatedBy;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}