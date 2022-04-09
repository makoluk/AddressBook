using AddressBook.API.Dtos;
using AddressBook.API.Entities;
using AddressBook.API.Filters;
using AddressBook.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelStateFilter))]
    public class ContactsController : CustomControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactsController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto contactCreateDto)
        {
            var contactEntity = _mapper.Map<Contact>(contactCreateDto);

            var result = await _contactService.CreateContact(contactEntity);

            return CreateActionResultInstance(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactDto contactUpdateDto)
        {
            var contactEntity = _mapper.Map<Contact>(contactUpdateDto);
            contactEntity.Id = id;

            var result = await _contactService.UpdateContact(contactEntity);

            return CreateActionResultInstance(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            return CreateActionResultInstance(await _contactService.DeleteContact(id));
        }

        [HttpGet("{searchText}")]
        public IActionResult Get(string searchText)
        {
            return CreateActionResultInstance(_contactService.GetBySearchText(searchText));
        }
    }
}