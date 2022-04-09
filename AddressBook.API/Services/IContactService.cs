using AddressBook.API.Dtos;
using AddressBook.API.Entities;
using AddressBook.API.Models;

namespace AddressBook.API.Services
{
    public interface IContactService
    {
        Task<Response<bool>> CreateContact(Contact contact);

        Task<Response<bool>> UpdateContact(Contact contact);

        Task<Response<bool>> DeleteContact(int id);

        Task<Contact> GetById(int id);

        Task<bool> IsContactIdExist(int id);

        Task<bool> IsContactNameExist(string name);

        Response<List<ContactDto>> GetBySearchText(string searchText);
    }
}