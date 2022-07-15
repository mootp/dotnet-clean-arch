using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role
        {
            Id = new Guid("83d9e4ac-b71b-49e6-9cbd-6eb30ce4aaa1"),
            Code = "SYSTEM",
            Desc = "System role",
            CreatedDate = DateTime.Now
        });
    }
}