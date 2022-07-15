using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = new Guid("d22e34c3-c426-4fc2-ae2a-6f5a330edc90"),
            Code = "ADMIN",
            Name = "Administrator",
            CreatedDate = DateTime.Now
        });
    }
}