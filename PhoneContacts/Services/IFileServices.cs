using PhoneContacts.Models;
using System.Collections.Generic;

namespace PhoneContacts.Services
{
    public interface IFileServices
    {
        void AddContact(PhoneContact contact);
        List<PhoneContact> Readlines();
        void SaveAllContacts(List<PhoneContact> contacts);
    }
}