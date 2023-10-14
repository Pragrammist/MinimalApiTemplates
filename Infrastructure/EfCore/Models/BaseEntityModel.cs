namespace EFCore.Models;

using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class EntityBase<TId>
{
    public TId Id { get; set; } = default!;
    
    public DateTime CreatedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; } // пользователь сам имзенил


    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }
}