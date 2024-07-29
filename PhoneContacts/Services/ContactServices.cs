using PhoneContacts.Models;
using phoneContactWithJSON.Services;

namespace PhoneContacts.Services
{
    public class ContactServices : IContactservices
    {
        private IFileServices fileServices;
        private ILoggingServices loggingServices;
        private List<PhoneContact> phoneContacts;

        public ContactServices()
        {
            Console.WriteLine("Choose file type: \n" +
                "1. JSON file \n" +
                "2. TXT file");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    fileServices = new FileServicesV2();
                    break;
                case "2":
                    fileServices = new FileServices();
                    break;
                default:
                    Console.WriteLine("Invalid choice! Defaulting to TXT file.");
                    fileServices = new FileServices();
                    break;
            }
            this.loggingServices = new LoggingServices();
            this.phoneContacts = fileServices.Readlines();
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            if (phoneContacts.Count == 0)
            {
                loggingServices.LogInfo
                    ("Your contacts list is empty.\n");

                return;
            }

            loggingServices.LogInfo("Your contact list:");
            for (int i = 0; i < phoneContacts.Count; i++)
            {
                loggingServices.LogInfo
                    ($"{i + 1}. Name: " +
                    $"{phoneContacts[i].Name}" +
                    $" Number: {phoneContacts[i].PhoneNumber}\n");
            }
        }

        public void SearchContact()
        {
            Console.Clear();
            loggingServices.LogInfo
                ("Enter a part of the phone number or name: ");

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                loggingServices.LogError
                    ("Input cannot be empty. Please try again.");

                return;
            }

            List<PhoneContact> contacts = phoneContacts.Where(contact =>
                contact.PhoneNumber.Contains(userInput)
                || contact.Name.Contains(userInput, 
                StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts.Count > 0)
            {
                foreach (var foundContact in contacts)
                {
                    loggingServices.LogInfo
                        ($"Name: {foundContact.Name} " +
                        $"Number: {foundContact.PhoneNumber}");
                }
            }
            else
            {
                loggingServices.LogError("No contacts found!");
            }
        }

        public void AddContact()
        {
            Console.Clear();
            bool isAdded = false;
            while (!isAdded)
            {
                try
                {
                    loggingServices.LogInfo("Enter name: ");
                    string newName = Console.ReadLine();

                    if (string.IsNullOrEmpty(newName))
                        throw new Exception("This space must be filled");

                    loggingServices.LogInfo("Enter number: ");
                    string newNumber = Console.ReadLine();
                    ValidatePhoneNumber(newNumber);

                    var newContact = new PhoneContact 
                    { 
                        Name = newName, 
                        PhoneNumber = newNumber 
                    };

                    if (phoneContacts.Exists
                        (contact => contact.PhoneNumber == newContact.PhoneNumber))
                        throw new ArgumentException
                            ("Contact with this phone number already exists.");

                    phoneContacts.Add(newContact);
                    fileServices.AddContact(newContact);
                    loggingServices.LogInfo("Contact added successfully!\n");
                    isAdded = true;
                }
                catch (Exception exc)
                {
                    loggingServices.LogError($"{exc.Message} Try again.");
                }
            }
        }

        public void EditContact()
        {
            Console.Clear();
            loggingServices.LogInfo
                ("Enter the name of the contact you want to edit: ");

            string userInput = Console.ReadLine();

            List<PhoneContact> contacts = 
                phoneContacts.Where(contact => 
                contact.Name.Contains
                (userInput, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts.Count == 0)
            {
                loggingServices.LogError
                    ("No contacts found with the given name.");

                return;
            }

            for (int i = 0; i < contacts.Count; i++)
            {
                loggingServices.LogInfo
                    ($"{i + 1}. Name: {contacts[i].Name}" +
                    $" Number: {contacts[i].PhoneNumber}");
            }

            loggingServices.LogInfo
                ("Enter the index of the contact you want to edit: ");

            if (int.TryParse(Console.ReadLine(), out int index)
                && index > 0 && index <= contacts.Count)
            {
                var contactToEdit = contacts[index - 1];
                try
                {
                    loggingServices.LogInfo("Enter new name: ");
                    string newName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newName))
                        throw new Exception
                            ("This space must be filled");

                    loggingServices.LogInfo("Enter new number: ");
                    string newNumber = Console.ReadLine();
                    ValidatePhoneNumber(newNumber);

                    contactToEdit.Name = newName;
                    contactToEdit.PhoneNumber = newNumber;

                    fileServices.SaveAllContacts(phoneContacts);

                    loggingServices.LogInfo
                        ("Contact edited successfully!\n");
                }
                catch (Exception exc)
                {
                    loggingServices.LogError(exc.Message);
                }
            }
            else
            {
                loggingServices.LogError("Invalid index.");
            }
        }

        public void DeleteContact()
        {
            Console.Clear();
            loggingServices.LogInfo
                ("Enter the name of the contact you want to delete: ");

            string userInput = Console.ReadLine();

            List<PhoneContact> contacts =
                phoneContacts.Where
                (contact => contact.Name.Contains
                (userInput, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts.Count == 0)
            {
                loggingServices.LogError
                    ("No contacts found with the given name.");

                return;
            }

            for (int i = 0; i < contacts.Count; i++)
            {
                loggingServices.LogInfo
                    ($"{i + 1}. Name: {contacts[i].Name}" +
                    $" Number: {contacts[i].PhoneNumber}");
            }

            loggingServices.LogInfo
                ("Enter the index of the contact you want to delete: ");

            if (int.TryParse(Console.ReadLine(),
                out int index) && index > 0 
                && index <= contacts.Count)
            {
                var contactToDelete = contacts[index - 1];
                phoneContacts.Remove(contactToDelete);

                fileServices.SaveAllContacts(phoneContacts);

                loggingServices.LogInfo
                    ("Contact deleted successfully!\n");
            }
            else
            {
                loggingServices.LogError("Invalid index.");
            }
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException
                    ("Phone number must be filled.");

            if (!phoneNumber.StartsWith("+"))
                throw new ArgumentException
                    ("Phone number must start with +.");

            if (phoneNumber.Length < 10 ||
                phoneNumber.Length > 14)
                throw new ArgumentException
                    ("Invalid phone number length.");
        }
    }
}
