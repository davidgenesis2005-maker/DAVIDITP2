namespace BankApp.Models
{
    public class User
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }

        public User(string name, string number, double initialBalance = 0)
        {
            AccountName = name;
            AccountNumber = number;
            Balance = initialBalance;
        }
    }
}