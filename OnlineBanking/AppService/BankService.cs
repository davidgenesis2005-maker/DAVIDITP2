using BankApp.Models;
using BankApp.DataServices;

namespace BankApp.AppService
{
    public class BankService
    {
        private readonly UserRepository _repo = new UserRepository();

        public void Deposit(User account, double amount)
        {
            if (amount > 0)
            {
                account.Balance += amount;
                _repo.UpdateBalance(account); // Save to MySQL
                System.Console.WriteLine("Deposit Successful!");
            }
        }

        public void Withdraw(User account, double amount)
        {
            if (amount > 0 && amount <= account.Balance)
            {
                account.Balance -= amount;
                _repo.UpdateBalance(account); // Save to MySQL
                System.Console.WriteLine("Withdrawal Successful!");
            }
        }

        public void SendMoney(User sender, User receiver, double amount)
        {
            if (amount > 0 && amount <= sender.Balance)
            {
                sender.Balance -= amount;
                receiver.Balance += amount;
                _repo.UpdateBalance(sender);   // Save sender
                _repo.UpdateBalance(receiver); // Save receiver
                System.Console.WriteLine("Transfer Successful!");
            }
        }
    }
}