namespace FleetManagementAPI.Models
{
    public class ManufacturedBy
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } // Name of the manufacturer (unique)
        public string Description { get; set; } // Optional description
        public bool IsLinkedToProduct { get; set; } // Indicates if linked to any product
    }
}
