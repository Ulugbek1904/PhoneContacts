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

        public void Home()
        {
            while (true)
            {
                string menu = "" +
                    "1. My Contacts\n" +
                    "2. Add contact\n" +
                    "3. delete contact\n" +
                    "4. Exit\n";

                this.loggingServices.LoggingInformation(
                    "====== Menu ======\n");

                this.loggingServices.
                    LoggingInformation(menu);

                this.loggingServices.
                    LoggingInformation("Choose one : ");
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
                            this.contactservices.AddContact();
                            break;
                        case 3:
                            this.contactservices.DeleteContact();
                            break;
                        case 4:
                            Exit();
                            break;
                        default:
                            throw new 
                                ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    loggingServices.LoggingError(
                        $"{exc.Message}. Try again");
                }
                catch (Exception exc)
                {
                    loggingServices.LoggingError(
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
