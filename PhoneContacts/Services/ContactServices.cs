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
                loggingServices.LoggingInformation(
                    "Your contacts list is empty.\n");

                return;
            }

            loggingServices.
                LoggingInformation("Your contact list:");

            for (int i = 0; i < phoneContacts.Count; i++)
            {
                loggingServices.LoggingInformation(
                    $"{i + 1}. Name: {phoneContacts[i].Name}" +
                    $" Number: {phoneContacts[i].PhoneNumber}\n");
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
                        LoggingInformation
                        ("Enter name: ");

                    string newName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newName))

                        throw new Exception
                            ("This space must be filled");
                    
                    loggingServices.
                        LoggingInformation
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
                        LoggingInformation
                        ("Contact added successfully!\n");
                    isAdded = true;
                }
                catch (ArgumentException exc)
                {
                    loggingServices.LoggingError($"{exc.Message} Try again");
                }
            }
        }

        public void DeleteContact()
        {
            Console.Clear();
            loggingServices.
                LoggingInformation("Enter" +
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

                    loggingServices.LoggingInformation
                        ("Contact deleted successfully!\n");
                }
                else
                    throw new ArgumentException("Invalid contact number");
            }
            catch(ArgumentException exc)
            {
                loggingServices.LoggingError(exc.Message);
            }
        }
    }
}
