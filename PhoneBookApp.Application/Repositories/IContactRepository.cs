using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Application.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    { 
        Task<Contact?> GetContactWithDetailsAsync(int contactId);
        Task<IEnumerable<Contact>> GetAllContactsWithDetailsAsync();
        Task<Contact?> FindByEmailAsync(string emailAddress);  
    } 
}
