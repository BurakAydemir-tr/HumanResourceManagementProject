using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Validation;

namespace WebAPI.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        private Type _validatorType;
        public ValidationFilterAttribute(Type validatorType) 
        {
            _validatorType = validatorType;
        }  
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidationHelper.Validate(_validatorType, context.ActionArguments.Values.ToArray());
            
            await next();
        }

        //private static void ValidationTool(IValidator validator,object entity)
        //{
        //    var context = new ValidationContext<object>(entity);
        //    var result = validator.Validate(context);
        //    if (!result.IsValid)
        //    {
        //        throw new ValidationException(result.Errors);
        //    }
        //}
    }
}
