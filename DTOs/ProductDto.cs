namespace FleetManagementAPI.DTOs
{
    public record ProductDto(
        int Id,
        string ProductCode,
        string ProductSKU,
        int ProductCategoryId,
        string ProductName,
        string ProductBarcode,
        int ProductSupplierId,
        int ProductManuNameId,
        int ProductUoMId,
        string ProductWarranty,
        DateTime ProductWarrantyStDate,
        DateTime ProductWarrantyEndDate,
        string ProductLocation,
        decimal ProductPriceUnit,
        int ProductOHQty,
        DateTime? ProductLastPurchaseDate, DateTime? ProductReceivedDate, DateTime? ProductLastApprovedDate
    );

    public record ProductListDto(
        int Id,
        string ProductCode,
        string ProductSKU,
        int ProductCategoryId,
        string ProductCategoryDescription,
        string ProductName,
        string ProductBarcode,
        int ProductSupplierId,
        string ProductSupplierCompanyName,
        int ProductManuNameId,
        int ProductUoMId,
        string ProductWarranty,
        DateTime ProductWarrantyStDate,
        DateTime ProductWarrantyEndDate,
        string ProductLocation,
        decimal ProductPriceUnit
    );
}
