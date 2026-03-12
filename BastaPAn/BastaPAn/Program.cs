using System;
using ClassLibrary2;
using ClassLibrary1;




namespace BankApp
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            BankAccount genesis = new BankAccount("Genesis", "ACC1001", 10000);
            BankAccount user123 = new BankAccount("user123", "ACC2002", 5000);

            Console.WriteLine("===== WELCOME TO ONLINE BANKING =====");
            Console.Write("Enter your account number: ");
            string inputAcc = Console.ReadLine();

            BankAccount currentUser = null;

            if (inputAcc == genesis.accountNumber)
            {
                currentUser = genesis;
            }
            else if (inputAcc == user123.accountNumber)
            {
                currentUser = user123;
            }
            else
            {
                Console.WriteLine("Account number not found. Exiting...");
                return;
            }

            Console.WriteLine($"Welcome, {currentUser.accountName}!");

            int choice = 0;

            while (choice != 6)
            {
                Console.WriteLine("\n===== ONLINE BANKING SYSTEM =====");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Send Money");
                Console.WriteLine("4. Receive Money");
                Console.WriteLine("5. Check Balance");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        double dep = Convert.ToDouble(Console.ReadLine());
                        currentUser.Deposit(dep);
                        break;

                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double wit = Convert.ToDouble(Console.ReadLine());
                        currentUser.Withdraw(wit);
                        break;

                    case 3:
                        Console.Write("Enter amount to send to user123: ");
                        double send = Convert.ToDouble(Console.ReadLine());
                        currentUser.SendMoney(user123, send);
                        break;

                    case 4:
                        Console.Write("Enter amount to receive from David: ");
                        double receive = Convert.ToDouble(Console.ReadLine());
                        user123.SendMoney(currentUser, receive);
                        break;

                    case 5:
                        currentUser.DisplayAccount();
                        break;

                    case 6:
                        Console.WriteLine("Thank you for using Online Banking!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}