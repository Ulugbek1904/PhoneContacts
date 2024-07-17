using PhoneContacts.Services;

namespace PhoneContacts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoggingServices 
                loggingServices = new LoggingServices();

            loggingServices.LoggingInformation("" +
                "\t\t\t\t Welcome to the Phone Contacts ");

            IHomeServices 
                homeServices = new HomeServices();

            homeServices.Home();
        }
    }
}
