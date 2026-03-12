using System;
using ClassLibrary2;

namespace ClassLibrary1
{
    public class BankOperations
    {
        public void Deposit(BankAccount account, double amount)
        {
            account.Deposit(amount);
        }

        public void Withdraw(BankAccount account, double amount)
        {
            account.Withdraw(amount);
        }

        public void SendMoney(BankAccount sender, BankAccount receiver, double amount)
        {
            sender.SendMoney(receiver, amount);
        }

        public void DisplayAccount(BankAccount account)
        {
            account.DisplayAccount();
        }
    }
}

