using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Core;
public class Role : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string? Desc { get; set; }

    [ForeignKey(nameof(RoleUser.RoleId))]
    public virtual ICollection<RoleUser>? Users { get; set; }

    [ForeignKey(nameof(RolePermission.RoleId))]
    public virtual ICollection<RolePermission>? Permissions { get; set; }
}