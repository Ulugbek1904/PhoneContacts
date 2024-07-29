using PhoneContacts.Services;

namespace PhoneContacts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoggingServices loggingServices
                = new LoggingServices();

            loggingServices.LogInfo("\t\t\t\t" +
                " Welcome to the Phone Contacts App");

            IContactservices contactServices = new ContactServices();
            Console.Clear();

            while (true)
            {
                string menu = "" +
                    "1. My Contacts\n" +
                    "2. Search contact\n" +
                    "3. Add contact\n" +
                    "4. Update contact\n" +
                    "5. Delete contact\n" +
                    "6. Exit\n";

                loggingServices.LogInfo
                    ("====== Menu ======\n");

                loggingServices.LogInfo(menu);

                loggingServices.LogInfo
                    ("Choose one: ");

                try
                {
                    string userInput = Console.ReadLine();
                    int intInput = int.Parse(userInput);

                    switch (intInput)
                    {
                        case 1:
                            contactServices.ShowAllContacts();
                            break;
                        case 2:
                            contactServices.SearchContact();
                            break;
                        case 3:
                            contactServices.AddContact();
                            break;
                        case 4:
                            contactServices.EditContact();
                            break;
                        case 5:
                            contactServices.DeleteContact();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    loggingServices.LogError($"{exc.Message}. Try again.");
                }
                catch (Exception exc)
                {
                    loggingServices.LogError($"{exc.Message}. Try again.");
                }
            }
        }
    }
}
