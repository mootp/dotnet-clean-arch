using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Core;
public class RoleUser : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Role))]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}