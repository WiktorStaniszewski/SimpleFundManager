namespace SimpleFundManager.Models;

public class Portfolio
{
    public List<Fund> Funds { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}
