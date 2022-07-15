using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Core;
public class RolePermission : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Role))]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(Permission))]
    public Guid PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;
}