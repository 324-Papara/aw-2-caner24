using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Para.Api;

public class BookValidationAttribute : IActionFilter
{

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Validasyon işlemi başarılı.");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionArguments = context.ActionArguments.Values;
        var dto = actionArguments.FirstOrDefault(arg => arg.GetType().Name.EndsWith("Book"));
        var validatorType = typeof(IValidator<>).MakeGenericType(dto.GetType());
        var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;
        var validationContext = new ValidationContext<object>(dto);
        var validationResult = validator.Validate(validationContext);

        if (!validationResult.IsValid)
        {
            var errorMessage = new
            {
                Errors = validationResult.Errors,
                StatusCode = 422
            };
            context.Result = new UnprocessableEntityObjectResult(errorMessage);
        }
    }

}