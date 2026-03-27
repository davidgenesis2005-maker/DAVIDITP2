using System;
using MySql.Data.MySqlClient;
using BankApp.Models;
using System.Linq;

namespace BankApp.DataServices
{
    public class UserRepository
    {
        private readonly DatabaseConfig _db = new DatabaseConfig();
        private readonly FileService _fileService = new FileService();

        public User GetByAccountNumber(string accNum)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    const string sql = "SELECT account_name, account_number, balance FROM users WHERE account_number = @accNum LIMIT 1";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@accNum", accNum);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User(
                                    reader.GetString("account_name"),
                                    reader.GetString("account_number"),
                                    reader.GetDouble("balance")
                                );
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($">> Database offline. Searching JSON backup... ({ex.Message})");

                var backupUsers = _fileService.GetAllFromBackup();
                return backupUsers.FirstOrDefault(u => u.AccountNumber == accNum);
            }
            return null;
        }

        public void Register(User user)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    const string sql = "INSERT INTO users (account_name, account_number, balance) VALUES (@name, @num, @bal)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", user.AccountName);
                        cmd.Parameters.AddWithValue("@num", user.AccountNumber);
                        cmd.Parameters.AddWithValue("@bal", user.Balance);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($">> DB Sync Error: {ex.Message}");
            }

            _fileService.SaveToBackup(user);
        }

        public bool AccountExists(string accNum)
        {
            return GetByAccountNumber(accNum) != null;
        }

        public void UpdateBalance(User user)
        {

            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    const string sql = "UPDATE users SET balance = @bal WHERE account_number = @num";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@bal", user.Balance);
                        cmd.Parameters.AddWithValue("@num", user.AccountNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($">> DB Update Error: {ex.Message}");
            }

            _fileService.SaveToBackup(user);
        }
    }
}