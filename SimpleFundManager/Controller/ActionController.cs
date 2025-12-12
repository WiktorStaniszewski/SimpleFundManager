using SimpleFundManager.Services;

namespace SimpleFundManager.Controller;

public class ActionController
{
    public void CreateFund(FundManager manager)
    {
        var fundName = ValidationService.ReadRequiredString("\nEnter the new fund name:");

        if (manager.FindFund(fundName))
        {
            ConsoleColorService.Warning("\nFund already exists. Adding a transaction instead.");
            ConsoleColorService.Info("\nEnter amount or type 0 to stop:");

            while (true)
            {
                var amount = ValidationService.ReadDecimalNCW();
                if (amount == 0) break;

                manager.AddTransaction(fundName, amount);
            }

            ConsoleColorService.Success($"{fundName}'s balance updated.");
        }
        else
        {
            var startingValue = ValidationService.ReadDecimal("\nEnter starting value:");
            manager.AddFund(fundName, startingValue);

            ConsoleColorService.Success($"{fundName} created with a starting value of {startingValue}.");
        }
    }

    public void CreateTransaction(FundManager manager)
    {
        if (!ValidationService.ConfirmExistingFunds(manager)) return;
        ConsoleColorService.Info("\nAvailable funds:");

        foreach (var f in manager.Funds)
            Console.WriteLine($"{f.Name}: {f.Value}");

        var name = ValidationService.ConfirmTheFund(manager, "\nEnter fund name:");

        ConsoleColorService.Info("\nEnter amount or type 0 to stop:");

        while (true)
        {
            var amount = ValidationService.ReadDecimalNCW();
            if (amount == 0) break;

            manager.AddTransaction(name, amount);
        }

        ConsoleColorService.Success($"{name}'s balance updated.");
    }

    public void ShowFunds(FundManager manager)
    {
        ConsoleColorService.Info("\nCurrent funds:");

        foreach (var f in manager.Funds)
            Console.WriteLine($"{f.Name}: {f.Value}");
        if (!ValidationService.ConfirmExistingFunds(manager)) return;
        var decision = ValidationService.ReadRequiredString("\nShow transactions? (t)");

        if (decision.ToLower() == "t")
        {
            var fundName = ValidationService.ConfirmTheFund(manager, "\nEnter fund name:");
            manager.ShowTransactions(fundName);
        }
        else
        {
            ConsoleColorService.Warning("Operation cancelled.");
        }
    }

    public void DeleteFund(FundManager manager)
    {
        if (!ValidationService.ConfirmExistingFunds(manager)) return; 
        ConsoleColorService.Warning("\nThis is irreversible.");
        var confirmation = ValidationService.ReadRequiredString("Are you sure? (y/n)");

        if (confirmation.ToLower() == "y")
        {
            ConsoleColorService.Info("\nExisting funds:");

            foreach (var f in manager.Funds)
                Console.WriteLine($"{f.Name}: {f.Value}");

            var fundToDelete = ValidationService.ConfirmTheFund(manager, "\nEnter fund to delete:");
            manager.RemoveFund(fundToDelete);

            ConsoleColorService.Success($"{fundToDelete} deleted.");
        }
        else
        {
            ConsoleColorService.Warning("Deletion cancelled.");
        }
    }

    public void QuitAndSave(FundManager manager)
    {
        manager.SaveToFile("portfolio.json");
        ConsoleColorService.Success("\nSaved. Quitting.");
        Environment.Exit(0);
    }
}
