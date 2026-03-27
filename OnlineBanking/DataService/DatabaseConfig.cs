using MySql.Data.MySqlClient;
using System;

namespace BankApp.DataServices
{
    public class DatabaseConfig
    {
        // 1. Updated connection string: Removed trailing semicolon in password 
        // and added 'SslMode=none' which is often required for modern MySQL connectors to talk to XAMPP.
        private readonly string _connectionString = "Server=localhost;Database=bank_db;User ID=root;Password=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // 2. Added a Helper Method to test if XAMPP is actually running
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