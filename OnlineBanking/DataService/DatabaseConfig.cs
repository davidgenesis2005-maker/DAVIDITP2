using MySql.Data.MySqlClient;
using System;

namespace BankApp.DataServices
{
    public class DatabaseConfig
    {

        private readonly string _connectionString = "Server=localhost;Database=bank_db;User ID=root;Password=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($">> XAMPP Connection Error: {ex.Message}");
                return false;
            }
        }
    }
}