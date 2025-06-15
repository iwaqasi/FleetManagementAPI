
using Microsoft.AspNetCore.Identity;
using System;

namespace FleetManagementAPI.Models
{
    public class Employee:IdentityUser
    {
        //public int Id { get; set; }
        public string EmployeeCode { get; set; } // Changed from nchar(10) to string
        //public string UserName { get; set; }
        //public string PasswordHash { get; set; } // Store hashed passwords
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Rights { get; set; } // Numeric representation of permissions
        public string EmployeeType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastLoginDate { get; set; } // Nullable for optional value
    }
}