using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Domain.Models;

namespace PhoneBookApp.Infrastructure.DataAccess
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.PhoneNumbers)
                .WithOne(p => p.Contact)
                .HasForeignKey(p => p.ContactID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Emails)
                .WithOne(e => e.Contact)
                .HasForeignKey(e => e.ContactID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhoneNumber>()
                .Property(p => p.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<Email>()
                .Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

        }
    }
}
