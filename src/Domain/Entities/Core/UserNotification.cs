using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Core;

public class UserNotification : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public string? Type { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Body { get; set; }
    public bool IsRead { get; set; } = false;
}