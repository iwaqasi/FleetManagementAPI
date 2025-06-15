// Models/Product.cs
using System;

namespace FleetManagementAPI.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary key

        public string ProductCode { get; set; } // Unique product code
        public string? ProductSKU { get; set; } // Unique SKU
        public int ProductCategoryId { get; set; } // Foreign key to Category
        public Category ProductCategory { get; set; } // Navigation property
        public string ProductName { get; set; } // Name of the product
        public string? ProductBarcode { get; set; } // Barcode of the product
        public int? ProductSupplierId { get; set; } // Foreign key to Supplier
        public Supplier ProductSupplier { get; set; } // Navigation property
        public int? ProductManuNameId { get; set; } // Foreign key to ManufacturedBy
        public ManufacturedBy ProductManuName { get; set; } // Navigation property
        public int ProductUoMId { get; set; } // Foreign key to UnitOfMeasure
        public UnitOfMeasure? ProductUoM { get; set; } // Navigation property
            
        public string? ProductWarranty { get; set; } // "Yes" or "No"
        public DateTime ProductWarrantyStDate { get; set; } // Warranty start date
        public DateTime ProductWarrantyEndDate { get; set; } // Warranty end date
        public int? ProductWarrantyDays { get; set; } // Calculated warranty days

        public string? ProductLocation { get; set; } // Inventory location
        public int ProductOHQty { get; set; } // On Hand Quantity
        public decimal ProductPriceUnit { get; set; } // Unit price
        public DateTime? ProductLastPurchaseDate { get; set; } // Last purchase date
        public DateTime? ProductReceivedDate { get; set; } // Last received date
        public DateTime? ProductLastApprovedDate { get; set; } // Last approved date

        public string? ProductCreatedBy { get; set; } // Created by user
    }
}