using SimpleFundManager.Models;
using System.Text.Json; 

namespace SimpleFundManager.Services;

public class FundManager
{
    private readonly Portfolio _portfolio = new();
    public IReadOnlyList<Fund> Funds => _portfolio.Funds;
    public IReadOnlyList<Transaction> Transactions => _portfolio.Transactions;

    internal Fund? GetFund(string name)
    {
        var result = _portfolio.Funds.FirstOrDefault(f =>
            f.Name.Equals(name));
        return result;
    }

    public int GetFundCount() => _portfolio.Funds.Count;

    public void AddFund(string name, decimal baseValue)
    {
        _portfolio.Funds.Add(new Fund(name, baseValue));
        LogService.Write($"Fund added: {name}, value: {baseValue}");
    }
    public void RemoveFund(string name)
    {
        var fund = GetFund(name);
        if (fund == null) {
            throw new ArgumentException("Fund not found.");
        }
        _portfolio.Funds.Remove(fund);
        _portfolio.Transactions.RemoveAll(t => t.FundName.Equals(name, StringComparison.OrdinalIgnoreCase));

        LogService.Write($"Fund removed: {name}");
    }
    public bool FindFund(string name) => GetFund(name) != null;


    public void AddTransaction(string fundName, decimal amount)
    {
        var fund = GetFund(fundName);
        if (fund == null)
            throw new ArgumentException("Fund not found.");

        if (amount == 0)
            return;

        fund.Value += amount;

        _portfolio.Transactions.Add(new Transaction(fundName, amount));
        LogService.Write($"Transaction added: {fundName}, amount: {amount}");
    }

    public void ShowTransactions(string fundName)
    {
        var transactions = _portfolio.Transactions
        .Where(t => t.FundName.Equals(fundName, StringComparison.OrdinalIgnoreCase))
        .OrderByDescending(t => t.Date)
        .Take(20)
        .ToList();

        if (!transactions.Any())
        {
            Console.WriteLine("No transactions found for this fund.");
            return;
        }

        Console.WriteLine($"Last {transactions.Count} transactions for {fundName}:");

        for (int i = 0; i < transactions.Count; i++)
        {
            var t = transactions[i];
            Console.WriteLine($"{i + 1}: Amount = {t.Amount}, Date = {t.Date:yyyy-MM-dd HH:mm:ss}");
        }
    }


    public void SaveToFile(string path)
    {
        var json = JsonSerializer.Serialize(_portfolio, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(path, json);
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path)) return;

        var json = File.ReadAllText(path);
        var loaded = JsonSerializer.Deserialize<Portfolio>(json);

        if (loaded != null)
        {
            _portfolio.Funds = loaded.Funds ?? new List<Fund>();
            _portfolio.Transactions = loaded.Transactions ?? new List<Transaction>();
        }
    }
    public string DisplayActionResult(string message)
    {
        Console.WriteLine($"\n{message}");
        return (message);
    }
}
