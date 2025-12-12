namespace SimpleFundManager.Models;

public class Fund
{
    public string Name { get; set; }
    public decimal Value { get; set; }

    public Fund(string name, decimal value)
    {
        Name = name;
        Value = value;
    }
}
