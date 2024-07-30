using System;

namespace PhoneContacts.Services
{
    internal class LoggingServices : ILoggingServices
    {
        public void LogError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;   
            Console.WriteLine(message);
        }
    }
}
