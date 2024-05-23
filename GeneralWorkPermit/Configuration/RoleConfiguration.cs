using GeneralWorkPermit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneralWorkPermit.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>

    {
       
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Inspector",
                    NormalizedName = "INSPECTOR"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
                );
        }
    }

    public class DefaultAdmin : IEntityTypeConfiguration<Admin>
    {
       
    public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(
                new Admin
                {
                    AdminType = AdminType.Inspector,
                    Id = "Admin",
                    UserId = "Admin",
                });
        }
    }

    public class DefaultUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = "Admin",
                    Email = "admin@gnp.com",
                    UserName = "admin@gnp.com",
                    FirstName = "Default",
                    LastName = "Admin",
                    Password = "P@ssw0rd",
                    UserType = UserType.Admin
                });
        }
    }
}
