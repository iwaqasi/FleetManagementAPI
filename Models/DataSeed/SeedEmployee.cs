using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagementAPI.Models.DataSeed
{
    public class SeedEmployee : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = "f3a0b5c4-1d2e-4b8e-9c3f-7a2d5e6f7a8b",
                    FirstName = "John",
                    LastName = "Doe",
                    AccessFailedCount = 0,
                    EmployeeCode = "EMP001",
                    CreationDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    UserName = "admin@mail.com",
                    NormalizedUserName = "ADMIN@MAIL.COM",
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEJC5/mQPXENvc/xOLQWevlsj5x/UDe7gf3/hrKAk8+L+BTWO/u2AeSz5CjJ38OiOYA==",
                    SecurityStamp = "HOCPUZCS7N5SSUYYRE257ZS2JEEIDEH3",
                    ConcurrencyStamp = "9dc54ff1-3f65-4e38-ae83-e83125155474",
                    EmployeeType = "Admin",
                    Rights = 1,
                    IsActive = true,
                    LastLoginDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    LockoutEnabled= false,
                });
        }
    }
}
