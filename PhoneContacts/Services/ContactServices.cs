using PhoneContacts.Models;

namespace PhoneContacts.Services
{
    public class ContactServices : IContactservices
    {
        private IFileServices fileServices;
        private ILoggingServices loggingServices;
        private List<PhoneContact> phoneContacts;

        public ContactServices()
        {
            this.fileServices = new FileServices();
            this.loggingServices = new LoggingServices();
            this.phoneContacts = fileServices.Readlines();
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            if (phoneContacts.Count == 0)
            {
                loggingServices.LogInfo(
                    "Your contacts list is empty.\n");

                return;
            }

            loggingServices.
                LogInfo("Your contact list:");

            for (int i = 0; i < phoneContacts.Count; i++)
            {
                loggingServices.LogInfo(
                    $"{i + 1}. Name: {phoneContacts[i].Name}" +
                    $" Number: {phoneContacts[i].PhoneNumber}\n");
            }
        }

        public void SearchContact()
        {
            Console.Clear();
            loggingServices.LogInfo
                ("Enter a part of phone number ");

            try
            {
                string userInput = Console.ReadLine();  
                if(!string.IsNullOrEmpty(userInput))
                {
                    List<PhoneContact> contacts = new List<PhoneContact>();
                    foreach(var contact in phoneContacts)
                    {
                        if (contact.PhoneNumber.Contains(userInput))
                        {
                            contacts.Add(contact);
                        }
                    }
                    if (contacts.Count > 0)
                    {
                        foreach (var foundContact in contacts)
                        {
                            loggingServices.LogInfo($"Name :" +
                                $" {foundContact.Name} Number : " +
                                $"{foundContact.PhoneNumber}");
                        }
                    }
                    else
                    {
                        loggingServices.LogError("No contacts found!");
                    }
                }
                else
                {
                    loggingServices.LogError
                        ("Input cannot be empty. " +
                        "Please try again.");
                }
            }
            catch (ArgumentException exp)
            {
                loggingServices.LogError($"{exp.Message} Try again");
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
                    loggingServices.
                        LogInfo
                        ("Enter name: ");

                    string newName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newName))

                        throw new Exception
                            ("This space must be filled");
                    
                    loggingServices.
                        LogInfo
                        ("Enter number: ");

                    string newNumber = Console.ReadLine();

                    if (string.IsNullOrEmpty(newNumber))
                        throw new ArgumentException
                            ("This space must be filled.");

                    else if (!newNumber.StartsWith("+"))
                        throw new ArgumentException
                            ("Phone Number must start with +.");

                    else if (newNumber.Length < 10
                            || newNumber.Length > 14)

                        throw new ArgumentException
                            ("Invalid phone number length.");

                    var newContact = new PhoneContact
                        { Name = newName, PhoneNumber = newNumber };

                    foreach(var contact in phoneContacts)
                    {
                        if (contact.PhoneNumber == newContact.PhoneNumber)
                        {
                            throw new ArgumentException("Already exists");
                        }
                    }
                    phoneContacts.Add(newContact);
                    fileServices.AddContact(newContact);

                    loggingServices.
                        LogInfo
                        ("Contact added successfully!\n");
                    isAdded = true;
                }
                catch (ArgumentException exc)
                {
                    loggingServices.LogError($"{exc.Message} Try again");
                }
            }
        }

        public void EditContact()
        {
            Console.Clear();

            loggingServices.
                LogInfo("Enter" +
                " the index of the contact " +
                "you want to Edit: ");

            try
            {
                string userInput = Console.ReadLine();
                int index = int.Parse(userInput);
                if (index > 0 && index <= phoneContacts.Count)
                {
                    index--;

                    loggingServices.
                        LogInfo
                        ("Enter new name: ");

                    string newName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newName))

                        throw new Exception
                            ("This space must be filled");

                    loggingServices.
                        LogInfo
                        ("Enter new  number: ");

                    string newNumber = Console.ReadLine();

                    if (string.IsNullOrEmpty(newNumber))
                        throw new ArgumentException
                            ("This space must be filled.");

                    else if (!newNumber.StartsWith("+"))
                        throw new ArgumentException
                            ("Phone Number must start with +.");

                    else if (newNumber.Length < 10
                            || newNumber.Length > 14)

                        throw new ArgumentException
                            ("Invalid phone number length.");

                    phoneContacts[index].Name = newName;
                    phoneContacts[index].PhoneNumber = newNumber;

                    // after Edited this method save data again in phoneContact.txt
                    fileServices.SaveAllContacts(phoneContacts);

                    loggingServices.LogInfo
                        ("Contact edited successfully!\n");
                }
                else
                    throw new ArgumentException("Invalid contact number");
            }
            catch (ArgumentException exc)
            {
                loggingServices.LogError(exc.Message);
            }
        }

        public void DeleteContact()
        {
            Console.Clear();
            loggingServices.
                LogInfo("Enter" +
                " the index of the contact " +
                "you want to delete: ");

            try
            {
                string userInput = Console.ReadLine();
                int index = int.Parse(userInput);
                if (index > 0 && index <= phoneContacts.Count)
                {
                    index--;
                    phoneContacts.RemoveAt(index);

                    // after deleted this method save data again in phoneContact.txt
                    fileServices.SaveAllContacts(phoneContacts);

                    loggingServices.LogInfo
                        ("Contact deleted successfully!\n");
                }
                else
                    throw new ArgumentException("Invalid contact number");
            }
            catch(ArgumentException exc)
            {
                loggingServices.LogError(exc.Message);
            }
        }
    }
}
