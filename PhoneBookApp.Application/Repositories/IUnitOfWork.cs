using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository Contacts { get; }
        IGenericRepository<PhoneNumber> PhoneNumbers { get; }
        IGenericRepository<Email> Emails { get; }

        Task<int> CompleteAsync();
    }
}
