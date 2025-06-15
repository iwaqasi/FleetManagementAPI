using FleetManagementAPI.Models;
using FleetManagementAPI.Models.DataSeed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FleetManagementAPI.Data
{
    public class FleetManagementContext : IdentityDbContext<Employee>
    {
        public FleetManagementContext(DbContextOptions<FleetManagementContext> options) : base(options)
        {
        }
        // Existing DbSets
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // New DbSet for UnitOfMeasure
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        // New DbSet for ManufacturedBy
        public DbSet<ManufacturedBy> ManufacturedBy { get; set; }

        // New DbSet for Product
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SeedEmployee()); // Apply the SeedEmployee configuration

            #region Employee Configuration
            modelBuilder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CreationDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserName)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeCode)
                .IsUnique();
            #endregion

            #region Category Configuration
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryCode) // Ensure CategoryCode is unique
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory) // Self-referencing relationship
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId) // Foreign key for sub-categories
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
            #endregion

            #region Supplier Configuration
            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Code) // Ensure Supplier Code is unique
                .IsUnique();

            modelBuilder.Entity<Supplier>()
                .Property(s => s.IsActive)
                .HasDefaultValue(true); // Default value for IsActive
            #endregion

            #region UnitOfMeasure Configuration
            modelBuilder.Entity<UnitOfMeasure>()
                .HasIndex(u => u.Name) // Ensure Name is unique
                .IsUnique();

            modelBuilder.Entity<UnitOfMeasure>()
                .Property(u => u.IsLinkedToProduct)
                .HasDefaultValue(false); // Default value for IsLinkedToProduct

           #endregion

            #region ManufacturedBy Configuration
            modelBuilder.Entity<ManufacturedBy>()
                .HasIndex(m => m.Name) // Ensure Name is unique
                .IsUnique();

            modelBuilder.Entity<ManufacturedBy>()
                .Property(m => m.IsLinkedToProduct)
                .HasDefaultValue(false); // Default value for IsLinkedToProduct
                       
            #endregion

            #region Product Configuration
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Primary key

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductCode) // Define index on ProductCode
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductSKU)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductSKU) // Define index on ProductSKU
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductWarranty)
                .HasConversion<string>() // Convert boolean to string ("Yes"/"No")
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductOHQty)
                .HasDefaultValue(0); // Default On Hand Quantity

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPriceUnit)
                .HasColumnType("decimal(18, 3)") // Decimal type for price
                .IsRequired();

            // Foreign Key Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany()
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductSupplier)
                .WithMany()
                .HasForeignKey(p => p.ProductSupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductManuName)
                .WithMany()
                .HasForeignKey(p => p.ProductManuNameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductUoM)
                .WithMany()
                .HasForeignKey(p => p.ProductUoMId)
                .OnDelete(DeleteBehavior.Restrict);

                       
            #endregion
        }
    }
}