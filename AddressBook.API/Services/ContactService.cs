using AddressBook.API.Data;
using AddressBook.API.Dtos;
using AddressBook.API.Entities;
using AddressBook.API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Services
{
    //Could be used repostiory classes for data access
    public class ContactService : IContactService
    {
        private readonly AddressBookContext _dbContext;
        private readonly IMapper _mapper;

        public ContactService(AddressBookContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateContact(Contact contact)
        {
            if (await IsContactNameExist(contact.Name))
            {
                Response<bool>.Fail("Contact is exist", 400);
            }

            await _dbContext.Contacts.AddAsync(contact);
            var status = await _dbContext.SaveChangesAsync();
            return status > 0 ? Response<bool>.Success(true, 200)
                : Response<bool>.Fail("Contact could not save", 500);
        }

        public async Task<Response<bool>> UpdateContact(Contact contact)
        {
            if (!await IsContactIdExist(contact.Id))
            {
                Response<bool>.Fail("Contact could not found", 404);
            }

            _dbContext.Entry(contact).State = EntityState.Modified;

            var status = await _dbContext.SaveChangesAsync();

            return status > 0 ? Response<bool>.Success(true, 200)
                : Response<bool>.Fail("Contact could not update", 500);
        }

        public async Task<Response<bool>> DeleteContact(int id)
        {
            var contact = await GetById(id);
            if (contact == null)
            {
                Response<bool>.Fail("Contact could not found", 404);
            }
            _dbContext.Contacts.Remove(contact);

            var status = await _dbContext.SaveChangesAsync();

            return status > 0 ? Response<bool>.Success(true, 200)
                : Response<bool>.Fail("Contact could not delete", 500);
        }

        public async Task<Contact> GetById(int id)
        {
            return await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsContactIdExist(int id)
        {
            if (await _dbContext.Contacts.AnyAsync(x => x.Id == id))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsContactNameExist(string name)
        {
            if (await _dbContext.Contacts.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public Response<List<ContactDto>> GetBySearchText(string searchText)
        {
            var query = _dbContext.Contacts.Where(x =>
            x.Name.Contains(searchText) ||
            x.Address.Contains(searchText) ||
            x.Phone.Contains(searchText) ||
            x.MobilePhone.Contains(searchText) ||
            x.Email.Contains(searchText)).AsNoTracking();

            var result = _mapper.Map<List<ContactDto>>(query.ToList());

            return Response<List<ContactDto>>.Success(result, 200);
        }
    }
}