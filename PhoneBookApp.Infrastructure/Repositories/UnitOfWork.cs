using PhoneBookApp.Application.Repositories;
using PhoneBookApp.Domain.Models;
using PhoneBookApp.Infrastructure.DataAccess;

namespace PhoneBookApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneBookDbContext _context;
        private IContactRepository? _contacts;
        private IGenericRepository<PhoneNumber>? _phoneNumbers;
        private IGenericRepository<Email>? _emails;

        public UnitOfWork(PhoneBookDbContext context)
        {
            _context = context;
        }

        public IContactRepository Contacts => _contacts ??= new ContactRepository(_context);
         
        public IGenericRepository<PhoneNumber> PhoneNumbers =>
            _phoneNumbers ??= new GenericRepository<PhoneNumber>(_context);

        public IGenericRepository<Email> Emails =>
            _emails ??= new GenericRepository<Email>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
