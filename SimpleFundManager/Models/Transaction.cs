using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleFundManager.Models;

public class Transaction
{
    public string FundName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }


    public Transaction(string fundName, decimal amount)
    {
        FundName = fundName;
        Amount = amount;
        Date = DateTime.Now;
    }
    public string ToLogString()
    {
        return $"Transaction: {FundName}, Amount: {Amount}, Date: {Date:yyyy-MM-dd HH:mm:ss}";
    }
}
