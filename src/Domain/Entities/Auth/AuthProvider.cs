using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Core;

namespace Domain.Entities.Auth;
public class AuthProvider : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public string Provider { get; set; } = null!;
    public string Token { get; set; } = null!;

    public void SetProvider(Guid userId, string provider, string token)
    {
        CreatedDate = DateTime.Now;
        CreatedId = userId;
        UserId = userId;
        Provider = provider;
        Token = token;

    }
}