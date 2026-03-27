using System;
using BankApp.Models;
using BankApp.AppService;
using BankApp.DataServices;

namespace BankApp.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository repo = new UserRepository();
            BankService bankService = new BankService();
            User currentUser = null;

            while (currentUser == null)
            {
                Console.WriteLine("\n===== BANKING SYSTEM PORTAL =====");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");
                string authChoice = Console.ReadLine();

                if (authChoice == "1")
                {
                    Console.Write("Enter Account Number: ");
                    string accNum = Console.ReadLine();
                    currentUser = repo.GetByAccountNumber(accNum);

                    if (currentUser == null)
                        Console.WriteLine(">> Error: Account not found!");
                }
                else if (authChoice == "2")
                {
                    Console.WriteLine("\n--- Create Your Account ---");
                    Console.Write("Enter Full Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Create Account Number: ");
                    string newAccNum = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newAccNum))
                    {
                        Console.WriteLine(">> Error: Account number cannot be empty.");
                    }
                    else if (repo.AccountExists(newAccNum))
                    {
                        Console.WriteLine($">> Error: The ID '{newAccNum}' is already taken!");
                    }
                    else
                    {
                        User newUser = new User(name, newAccNum, 0);
                        repo.Register(newUser);

                        Console.WriteLine("\n**********************************");
                        Console.WriteLine("  ACCOUNT CREATED SUCCESSFULLY!");
                        Console.WriteLine($"  Name: {newUser.AccountName}");
                        Console.WriteLine($"  Your Account ID: {newUser.AccountNumber}");
                        Console.WriteLine("**********************************");

                        currentUser = newUser;
                        Console.WriteLine("Logging you in automatically...");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
                else if (authChoice == "3") return;
            }

            int choice = 0;
            while (choice != 6)
            {
                Console.WriteLine($"\n--- Welcome, {currentUser.AccountName} ---");
                Console.WriteLine("1. Deposit  2. Withdraw  3. Send Money  4. Check Balance  6. Logout");
                Console.Write("Action: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1:
                        Console.Write("Deposit Amount: ");
                        bankService.Deposit(currentUser, double.Parse(Console.ReadLine()));
                        break;
                    case 2:
                        Console.Write("Withdraw Amount: ");
                        bankService.Withdraw(currentUser, double.Parse(Console.ReadLine()));
                        break;
                    case 3:
                        Console.Write("Recipient Account Number: ");
                        string recAcc = Console.ReadLine();
                        User recipient = repo.GetByAccountNumber(recAcc);

                        if (recipient != null)
                        {
                            Console.Write("Amount: ");
                            bankService.SendMoney(currentUser, recipient, double.Parse(Console.ReadLine()));
                        }
                        else Console.WriteLine("Recipient not found.");
                        break;
                    case 4:
                        Console.WriteLine($"Account: {currentUser.AccountName}");
                        Console.WriteLine($"Number:  {currentUser.AccountNumber}");
                        Console.WriteLine($"Balance: PHP {currentUser.Balance}");
                        break;
                }
            }
            Console.WriteLine("Logged out. Have a great day!");
        }
    }
}