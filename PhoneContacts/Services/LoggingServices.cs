namespace PhoneContacts.Services
{
    internal class LoggingServices : ILoggingServices
    {
        public void LoggingError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public void LoggingInformation(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;   
            Console.WriteLine(message);
        }
    }
}
