using PhoneContacts.Models;

namespace PhoneContacts.Services
{
    public interface IFileServices
    {
        void AddContact(PhoneContact contact);
        List<PhoneContact> Readlines();
        void SaveAllContacts(List<PhoneContact> contacts);
    }
}