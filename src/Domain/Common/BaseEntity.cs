using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity
{
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedId { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedId { get; set; }
}