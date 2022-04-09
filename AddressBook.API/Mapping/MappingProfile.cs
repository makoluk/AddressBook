using AddressBook.API.Dtos;
using AddressBook.API.Entities;
using AutoMapper;

namespace AddressBook.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}