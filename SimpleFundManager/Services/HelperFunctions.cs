using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFundManager.Services;

public class HelperFunctions
{
    public static void PrintFundList(FundManager manager, string header = "\nCurrent funds:")
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
}
