// FleetManagementAPI/Models/Supplier.cs
using System.ComponentModel.DataAnnotations;

namespace FleetManagementAPI.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(255)]
        public string? CompanyName { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone1 { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone2 { get; set; }
        [MaxLength(255)]
        public string? Address1 { get; set; }
        [MaxLength(255)]
        public string? Address2 { get; set; }

        public bool IsActive { get; set; } = true;
    }
}