using SimpleFundManager.Controller;
using SimpleFundManager.Services;

internal class Program
{
    private static void Main()
    {
        var manager = new FundManager();
        var controller = new ActionController();

        manager.LoadFromFile("portfolio.json");

        while (true)
        {
            ConsoleColorService.Info("\nSelect your action:\n");
            Console.WriteLine("(f) add a fund");
            Console.WriteLine("(t) add a transaction");
            Console.WriteLine("(m) move money (transfer)");
            Console.WriteLine("(s) show funds/transactions");
            Console.WriteLine("(p) project growth");
            Console.WriteLine("(d) delete funds");
            Console.WriteLine("(q) quit and save\n");

            ConsoleColorService.Prompt("Choice: ");

            try
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    throw new Exception("Invalid choice.");

                switch (char.ToLower(input[0]))
                {
                    case 'f': controller.CreateFund(manager); break;
                    case 't': controller.CreateTransaction(manager); break;
                    case 'm': controller.TransferFunds(manager); break;
                    case 's': controller.ShowFunds(manager); break;
                    case 'p': controller.ProjectGrowth(manager); break;
                    case 'd': controller.DeleteFund(manager); break;
                    case 'q': controller.QuitAndSave(manager); break;

                    default:
                        throw new Exception("Invalid choice.");
                }
            }
            catch (Exception ex)
            {
                ConsoleColorService.Error(ex.Message);
                LogService.Write($"Exception: {ex.Message}");
            }
        }
    }
}
