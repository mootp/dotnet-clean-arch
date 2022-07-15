using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Core;

namespace Domain.Entities.Auth;
public class AuthLocal : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}