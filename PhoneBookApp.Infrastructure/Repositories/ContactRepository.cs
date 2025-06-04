using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Application.Repositories;
using PhoneBookApp.Domain.Models;
using PhoneBookApp.Infrastructure.DataAccess;

namespace PhoneBookApp.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PhoneBookDbContext context) : base(context)
        {

        }

        public async Task<Contact?> GetContactWithDetailsAsync(int contactId)
        {
            return await _dbSet
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Emails)
                .SingleOrDefaultAsync(c => c.ContactID == contactId);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsWithDetailsAsync()
        {
            return await _dbSet
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Emails)
                .ToListAsync();
        }

        public async Task<Contact?> FindByEmailAsync(string emailAddress)
        {
            return await _dbSet
                .Include(c => c.Emails)
                .FirstOrDefaultAsync(c => c.Emails.Any(e => e.Address.ToLower() == emailAddress.ToLower()));
        }

        public override async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _dbSet.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
