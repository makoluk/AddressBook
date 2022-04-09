using AddressBook.API.Dtos;
using FluentValidation;

namespace AddressBook.API.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.MobilePhone).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}