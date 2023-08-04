using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIFinalProject.Configurations.Entites
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        void IEntityTypeConfiguration<IdentityRole>.Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName="USER"
            },new IdentityRole
            {
                 Name="Admin",
                 NormalizedName="ADMIN"
            },new IdentityRole
            {
                Name="Representative",
                NormalizedName="REPRESENTATIVE"
            }
            );
        }
    }
}
