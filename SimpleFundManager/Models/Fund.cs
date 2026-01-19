namespace SimpleFundManager.Models;

public class Fund
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public decimal TargetValue { get; set; }

    public Fund(string name, decimal value, decimal targetValue)
    {
        Name = name;
        Value = value;
        TargetValue = targetValue;
    }
}
