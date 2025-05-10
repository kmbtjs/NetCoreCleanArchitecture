using App.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.WebAPI.Filters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Perform validation logic here
            // If validation fails, set context.Result to an appropriate result

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorResponse = ServiceResult.Failure(errors);

                context.Result = new BadRequestObjectResult(errorResponse);

                return;
            }

            await next();
        }
    }
}
