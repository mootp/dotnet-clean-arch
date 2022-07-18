using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Core;

namespace Domain.Entities.Auth;
public class AuthSession : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public string? Desc { get; set; }
    public string Provider { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;

    public void SetSession(Guid userId, string refreshToken)
    {
        CreatedDate = DateTime.Now;
        CreatedId = userId;
        UserId = userId;
        RefreshToken = refreshToken;
    }


}