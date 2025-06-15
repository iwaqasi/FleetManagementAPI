using System.ComponentModel.DataAnnotations;

namespace FleetManagementAPI.Models
{
    public class SupplierStatusUpdateModel
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
