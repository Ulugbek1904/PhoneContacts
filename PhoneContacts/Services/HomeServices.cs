namespace PhoneContacts.Services
{
    internal class HomeServices : IHomeServices
    {
        IContactservices contactservices;
        IFileServices fileServices;
        ILoggingServices loggingServices;
        public HomeServices()
        {
            this.contactservices = new ContactServices();
            this.fileServices = new FileServices();
            this.loggingServices = new LoggingServices();
        }

        public void LoadMenu()
        {
            while (true)
            {
                string menu = "" +
                    "1. My Contacts\n" +
                    "2. Search contact\n" +
                    "3. Add contact\n" +
                    "4. update contact\n" +
                    "5. delete contact\n" +
                    "6. Exit\n";

                this.loggingServices.LogInfo(
                    "====== Menu ======\n");

                this.loggingServices.
                    LogInfo(menu);

                this.loggingServices.
                    LogInfo("Choose one : ");
                try
                {
                    string userInput = Console.ReadLine();
                    int intInput = int.Parse(userInput);
                    switch (intInput)
                    {
                        case 1:
                            this.contactservices.ShowAllContacts();
                            break;
                        case 2:
                            this.contactservices.SearchContact();
                            break;
                        case 3:
                            this.contactservices.AddContact();
                            break;
                        case 4:
                            this.contactservices.EditContact();
                            break;
                        case 5:
                            this.contactservices.DeleteContact();
                            break;
                        case 6:
                            Exit();
                            break;
                        default:
                            throw new 
                                ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    loggingServices.LogError(
                        $"{exc.Message}. Try again");
                }
                catch (Exception exc)
                {
                    loggingServices.LogError(
                        $"{exc.Message}. Try again");
                }
            }
        }
        
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
