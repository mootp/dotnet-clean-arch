using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Data.Configs;

public class AuthLocalConfig : IEntityTypeConfiguration<AuthLocal>
{
    public void Configure(EntityTypeBuilder<AuthLocal> builder)
    {
        builder.HasData(new AuthLocal
        {
            Id = new Guid("468d5f1f-c8fc-4049-ab3b-5e7e3986eb01"),
            UserId = new Guid("d22e34c3-c426-4fc2-ae2a-6f5a330edc90"),
            Username = "superadmin",
            PasswordHash = BC.HashPassword("P@ssw0rd"),
            CreatedDate = DateTime.Now
        });
    }
}