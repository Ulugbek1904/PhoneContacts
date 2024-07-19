using PhoneContacts.Services;

namespace PhoneContacts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoggingServices 
                loggingServices = new LoggingServices();

            loggingServices.LogInfo("" +
                "\t\t\t\t Welcome to the Phone Contacts App");

            IHomeServices 
                homeServices = new HomeServices();

            homeServices.LoadMenu();
        }
    }
}
