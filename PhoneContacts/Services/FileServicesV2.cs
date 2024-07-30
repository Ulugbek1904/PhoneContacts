using PhoneContacts.Models;
using PhoneContacts.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace phoneContactWithJSON.Services
{
    internal class FileServicesV2 : IFileServices
    {
        private const string filePath = "../../../phoneContacts.json";
        private ILoggingServices loggingServices;

        public FileServicesV2()
        {
            this.loggingServices = new LoggingServices();
            EnsureFileExist();
        }

        public List<PhoneContact> Readlines()
        {
            try
            {
                if (new FileInfo(filePath).Length == 0)
                {
                    return new List<PhoneContact>();
                }

                var fileContent = File.ReadAllText(filePath);
                var contacts = JsonSerializer.Deserialize<List<PhoneContact>>(fileContent);
                return contacts ?? new List<PhoneContact>();
            }
            catch (JsonException ex)
            {
                loggingServices.LogError($"JSON deserialization error: {ex.Message}");
                return new List<PhoneContact>();
            }
        }

        public void AddContact(PhoneContact contact)
        {
            var contacts = Readlines();
            contacts.Add(contact);

            var jsonString = JsonSerializer.Serialize(contacts);
            File.WriteAllText(filePath, jsonString);
        }

        public void SaveAllContacts(List<PhoneContact> contacts)
        {
            var jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        private static void EnsureFileExist()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllText(filePath, "[]"); 
            }
        }
    }
}
