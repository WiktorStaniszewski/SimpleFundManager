using SimpleFundManager.Services;

public static class ValidationService
{
    public static decimal ReadDecimal(string message)
    {
        ConsoleColorService.Info(message);

        while (true)
        {
            ConsoleColorService.Prompt("> ");

            var input = Console.ReadLine();
            if (decimal.TryParse(input, out var result))
                return result;

            ConsoleColorService.Warning("Invalid numeric input. Try again.");
            LogService.Write($"Invalid numeric input: {input}");
        }
    }

    public static decimal ReadDecimalNCW()
    {
        ConsoleColorService.Prompt("> ");

        var input = Console.ReadLine();
        if (!decimal.TryParse(input, out var result))
        {
            ConsoleColorService.Warning("Invalid input. Cancelling operation.");
            LogService.Write($"Invalid decimal: {input}");
            return 0;
        }

        return result;
    }

    public static string ReadRequiredString(string message)
    {
        ConsoleColorService.Info(message);

        while (true)
        {
            ConsoleColorService.Prompt("> ");

            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();


            ConsoleColorService.Warning("Input cannot be empty.");
            LogService.Write("Empty required string input.");
        }
    }

    public static string ConfirmTheFund(FundManager manager, string message)
    {
        ConsoleColorService.Info(message);

        while (true)
        {
            ConsoleColorService.Prompt("> ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColorService.Warning("Input cannot be empty. Try again.");
                continue;
            }

            if (manager.FindFund(name))
                return name;

            ConsoleColorService.Warning("Fund not found. Try again.");
            LogService.Write($"Fund not found: {name}");
        }
    }

    public static bool ConfirmExistingFunds(FundManager manager)
    {
        if (manager.GetFundCount() == 0)
        {
            ConsoleColorService.Warning("\nNo existing funds.");
            return false;
        }
        return true;
    }
}
