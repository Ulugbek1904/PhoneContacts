namespace PhoneContacts.Services
{
    public interface ILoggingServices
    {
        void LoggingInformation(string message);
        void LoggingError(string message);
    }
}