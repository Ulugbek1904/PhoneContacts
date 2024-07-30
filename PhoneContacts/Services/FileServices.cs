using PhoneContacts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneContacts.Services
{
    internal class FileServices : IFileServices
    {
        private const string filePath = 
            "../../../phoneContacts.txt";

        private ILoggingServices loggingServices;
        public FileServices()
        {
            this.loggingServices =
                new LoggingServices();

            EnsureFileExist();
        }

        private static void EnsureFileExist()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public List<PhoneContact> Readlines()
        {
            List<PhoneContact> phoneContacts = new List<PhoneContact>();

            string[] allLines = File.ReadAllLines(filePath);

            foreach (string line in allLines)
            {
                var contact = new PhoneContact();
                var words = line.Split(' ');

                if (words.Length > 2 && words[0] == "Name:")
                {
                    // Extracting the name and phone number correctly
                    contact.Name = words[1];
                    contact.PhoneNumber = words[3];
                }

                phoneContacts.Add(contact);
            }

            return phoneContacts;
        }

        public void AddContact(PhoneContact contact)
        {
            var contactLine = $"Name: {contact.Name}; " +
                $"Number: {contact.PhoneNumber}";

            File.AppendAllText(filePath, 
                contactLine + Environment.NewLine);
        }

        public void SaveAllContacts(List<PhoneContact> contacts)
        {
            var lines = contacts.Select(contact => 
            $"Name: {contact.Name} Number: " +
            $"{contact.PhoneNumber}");

            File.WriteAllLines(filePath, lines);
        }
    }
}
