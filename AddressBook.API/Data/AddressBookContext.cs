using AddressBook.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Data
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}