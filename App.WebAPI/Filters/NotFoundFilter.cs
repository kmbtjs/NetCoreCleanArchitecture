using App.Application;
using App.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.WebAPI.Filters
{
    public class NotFoundFilter<T, Tid>(IGenericRepository<T, Tid> genericRepository) : Attribute, IAsyncActionFilter where T : class where Tid : struct
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments.TryGetValue("id", out var idAsObj) ? idAsObj : null;

            if (idAsObj is not Tid idValue)
            {
                await next();
                return;
            }

            if (await genericRepository.AnyAsync(idValue))
            {
                await next();
                return;
            }

            var entityName = typeof(T).Name;
            var actionMethod = context.ActionDescriptor.RouteValues["action"];

            var result = ServiceResult.Failure($"The {entityName} with id {idValue} was not found in the {actionMethod} action.");

            context.Result = new NotFoundObjectResult(result);
        }
    }
}
