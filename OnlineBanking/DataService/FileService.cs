using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BankApp.Models;

namespace BankApp.DataServices
{
    public class FileService
    {
        private readonly string _filePath = "bank_data_backup.json";

        public void SaveToBackup(User user)
        {
            List<User> users = GetAllFromBackup();

            // Check if user already exists in JSON to avoid duplicates
            int index = users.FindIndex(u => u.AccountNumber == user.AccountNumber);
            if (index != -1)
                users[index] = user; // Update existing
            else
                users.Add(user); // Add new

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<User> GetAllFromBackup()
        {
            if (!File.Exists(_filePath)) return new List<User>();

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
    }
}