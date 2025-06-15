// Models/CategoryDto.cs
namespace FleetManagementAPI.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<CategoryDto> SubCategories { get; set; }
    }
}