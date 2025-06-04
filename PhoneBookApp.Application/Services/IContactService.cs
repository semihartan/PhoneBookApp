using PhoneBookApp.DTOs;

namespace PhoneBookApp.Application.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactReadDto>> GetAllContactsAsync();
        Task<ContactReadDto?> GetContactByIdAsync(int id);
        Task<ContactReadDto> CreateContactAsync(ContactCreateDto contactCreateDto);
        Task<bool> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto);
        Task<bool> DeleteContactAsync(int id);
    }
}
