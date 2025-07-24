namespace BankAccount.Models;

public class BankAccountDto
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
}