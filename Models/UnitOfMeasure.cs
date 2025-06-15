using System.ComponentModel.DataAnnotations;

namespace FleetManagementAPI.Models
{
    public class UnitOfMeasure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } // e.g., "Kilogram", "Liter"

        [StringLength(255)]
        public string? Description { get; set; } // Optional description

        [Required]
        public bool IsLinkedToProduct { get; set; } = false; // Default to false
    }
}