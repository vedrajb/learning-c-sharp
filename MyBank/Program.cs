using System;

namespace MyBank
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var account = new BankAccount("Vedraj", 10000);
                Console.WriteLine($"Acc# '{account.Number}' " +
                    $"was created for '{account.Owner}' " +
                    $"with '{account.Balance}' initial balance.");

                account.MakeWithdrawal(600, DateTime.Now, "PS4");
                //Console.WriteLine($"Acc# '{account.Number}' balance: {account.Balance}");
                account.MakeDeposit(1000, DateTime.Now, "Salary");
                //Console.WriteLine($"Acc# '{account.Number}' balance: {account.Balance}");
                account.MakeWithdrawal(60, DateTime.Now, "item1");
                account.MakeWithdrawal(155, DateTime.Now, "item3");
                account.MakeWithdrawal(74, DateTime.Now, "item2");

                var history = account.GetTransactionHistory();
                Console.WriteLine(history);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.GetType()}\n\tMessage: {ex.Message}");
                return;
            }
        }
    }
}
