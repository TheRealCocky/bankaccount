using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models;

public class BankAccountModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public string Type { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
}