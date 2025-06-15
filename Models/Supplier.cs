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

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone1 { get; set; }

        [Phone]
        public string Phone2 { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public bool IsActive { get; set; } = true;
    }
}