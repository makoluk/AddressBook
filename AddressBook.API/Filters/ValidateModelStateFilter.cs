using AddressBook.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AddressBook.API.Filters
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var validationErrors = context.ModelState.Keys
                .SelectMany(i => context.ModelState[i].Errors)
                .Select(i => i.ErrorMessage)
                .ToList();

            context.Result = new BadRequestObjectResult(Response<NoContent>.Fail(validationErrors, 400));
        }
    }
}