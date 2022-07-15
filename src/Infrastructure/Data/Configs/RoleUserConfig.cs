using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class RoleUserConfig : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.HasData(new RoleUser
        {
            Id = new Guid("04625b27-334a-45de-8f3f-3d47be31516b"),
            RoleId = new Guid("83d9e4ac-b71b-49e6-9cbd-6eb30ce4aaa1"),
            UserId = new Guid("d22e34c3-c426-4fc2-ae2a-6f5a330edc90")
        });
    }
}