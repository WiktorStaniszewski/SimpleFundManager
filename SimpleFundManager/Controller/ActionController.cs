using SimpleFundManager.Services;

namespace SimpleFundManager.Controller;

public class ActionController
{
    private static void PrintFundList(FundManager manager, string header = "\nCurrent funds:")
    {
        ConsoleColorService.Info(header);

        if (manager.Funds.Count == 0)
        {
            Console.WriteLine("  (No funds available)");
            return;
        }

        foreach (var f in manager.Funds)
        {
            string status = "";
            // Gamification Logic (Goal Progress)
            if (f.TargetValue > 0)
            {
                decimal percent = f.Value == 0 ? 0 : (f.Value / f.TargetValue) * 100;
                if (percent < 0) percent = 0;
                status = $" [Goal: {f.TargetValue} - {percent:F1}% Reached]";
            }
            Console.WriteLine($"{f.Name}: {f.Value}{status}");
        }
    }






    public void CreateFund(FundManager manager)
    {
        var fundName = ValidationService.ReadRequiredString("\nEnter the new fund name:");

        if (manager.FindFund(fundName))
        {
            ConsoleColorService.Warning("\nFund already exists. Adding a transaction instead.");
            ConsoleColorService.Info("\nEnter amount or type 0 (or 'exit') to stop:");

            while (true)
            {
                var amount = ValidationService.ReadDecimalOrExit();
                if (amount == 0) break;

                manager.AddTransaction(fundName, amount);
            }

            ConsoleColorService.Success($"{fundName}'s balance updated.");
        }
        else
        {
            var startingValue = ValidationService.ReadDecimal("\nEnter starting value:");

            var targetValue = ValidationService.ReadDecimal("\nEnter target goal (type 0 for none):");

            manager.AddFund(fundName, startingValue, targetValue);

            ConsoleColorService.Success($"{fundName} created with starting value {startingValue} and goal {targetValue}.");
        }
    }

    public void ProjectGrowth(FundManager manager)
    {
        PrintFundList(manager);

        if (!ValidationService.ConfirmExistingFunds(manager)) return;

        ConsoleColorService.Info("\n--- Project Future Growth ---");

        var fundName = ValidationService.ConfirmTheFund(manager, "Select a fund to project:");
        if (fundName == null) return;

        var fund = manager.GetFund(fundName);

        var rateInput = ValidationService.ReadDecimal("Enter expected annual interest rate % (e.g., 5 for 5%):");
        var years = (int)ValidationService.ReadDecimal("Enter number of years:");

        // 3. Calculate (Compound Interest Formula: A = P(1 + r)^t)
        // We assume annual compounding (n=1) for simplicity.
        double rate = (double)rateInput / 100.0;
        double principal = (double)fund.Value;

        double futureValue = principal * Math.Pow((1 + rate), years);
        decimal profit = (decimal)futureValue - fund.Value;

        Console.WriteLine($"\n--- Projection for {fund.Name} ---");
        Console.WriteLine($"Current Value: {fund.Value:C}");
        Console.WriteLine($"Rate: {rateInput}% | Years: {years}");
        ConsoleColorService.Success($"Future Value: {futureValue:N2}");
        ConsoleColorService.Info($"Total Growth: +{profit:N2}");
        Console.WriteLine("(Note: This is a simulation. Actual values not changed.)");
    }

    public void CreateTransaction(FundManager manager)
    {
        if (!ValidationService.ConfirmExistingFunds(manager)) return;

        PrintFundList(manager, "\nAvailable funds:");

        var name = ValidationService.ConfirmTheFund(manager, "\nEnter fund name:");
        if (name == null)
        {
            ConsoleColorService.Warning("Operation cancelled.");
            return;
        }

        ConsoleColorService.Info("\nEnter amount or type 0 (or 'exit') to stop:");

        while (true)
        {
            var amount = ValidationService.ReadDecimalOrExit();
            if (amount == 0) break;

            manager.AddTransaction(name, amount);
        }

        ConsoleColorService.Success($"{name}'s balance updated.");
    }

    public void ShowFunds(FundManager manager)
    {
        ConsoleColorService.Info("\nCurrent funds:");

        PrintFundList(manager);

        Console.WriteLine(new string('-', 30));
        ConsoleColorService.Info($"Total Portfolio Value:   {manager.GetTotalValue()}");
        ConsoleColorService.Info($"Total Transaction Count: {manager.GetTotalTransactionCount()}");
        Console.WriteLine(new string('-', 30));

        if (!ValidationService.ConfirmExistingFunds(manager)) return;

        var fundName = ValidationService.ConfirmTheFund(manager, "\nEnter fund name to see transactions:");
        if (fundName != null)
        {
            manager.ShowTransactions(fundName);
        }
    }

    public void DeleteFund(FundManager manager)
    {
        if (!ValidationService.ConfirmExistingFunds(manager)) return;

        PrintFundList(manager, "\nSelect fund to delete:");

        var fundToDelete = ValidationService.ConfirmTheFund(manager, "\nEnter fund to delete:");

        if (fundToDelete == null)
        {
            ConsoleColorService.Warning("Deletion cancelled.");
            return;
        }

        var fundObj = manager.Funds.FirstOrDefault(f => f.Name.Equals(fundToDelete, StringComparison.OrdinalIgnoreCase));

        ConsoleColorService.Warning($"\nWARNING: You are about to delete '{fundToDelete}'");
        ConsoleColorService.Warning($"Current Balance: {fundObj?.Value}");
        ConsoleColorService.Warning("This action is irreversible.");

        var confirmation = ValidationService.ReadRequiredString("Are you sure? (Type 'yes' to confirm)");

        if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            manager.RemoveFund(fundToDelete);
            ConsoleColorService.Success($"{fundToDelete} deleted.");
        }
        else
        {
            ConsoleColorService.Info("Deletion cancelled.");
        }
    }

    public void TransferFunds(FundManager manager)
    {
        if (!ValidationService.ConfirmExistingFunds(manager)) return;

        PrintFundList(manager, "\n--- Transfer Money ---");

        var sourceName = ValidationService.ConfirmTheFund(manager, "\nEnter SOURCE fund name:");
        if (sourceName == null) return;

        var destName = ValidationService.ConfirmTheFund(manager, "\nEnter DESTINATION fund name:");
        if (destName == null) return;

        ConsoleColorService.Info($"\nHow much to move from {sourceName} to {destName}?");


        while (true)
        {
            var amount = ValidationService.ReadDecimalOrExit();
            if (amount == 0)
            {
                ConsoleColorService.Warning("Transfer cancelled.");
                return;
            }

            try
            {
                manager.Transfer(sourceName, destName, amount);
                ConsoleColorService.Success($"Successfully moved {amount} from {sourceName} to {destName}.");
                break;
            }
            catch (Exception ex)
            {
                ConsoleColorService.Error(ex.Message);
                ConsoleColorService.Info("Enter a valid amount or type 0 to cancel:");
            }
        }
    }

    public void QuitAndSave(FundManager manager)
    {
        manager.SaveToFile("portfolio.json");
        ConsoleColorService.Success("\nSaved. Quitting.");
        Environment.Exit(0);
    }
}