namespace PhoneContacts.Services
{
    public interface ILoggingServices
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}