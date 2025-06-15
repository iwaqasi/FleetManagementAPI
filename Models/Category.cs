// Models/Category.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagementAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryCode { get; set; } // Unique identifier for the category

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } // Description of the category

        // Self-referencing relationship for sub-categories
        public int? ParentCategoryId { get; set; } // Nullable foreign key for sub-categories

        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; } // Navigation property for parent category

        public virtual ICollection<Category>? SubCategories { get; set; } // Navigation property for sub-categories
    }
}