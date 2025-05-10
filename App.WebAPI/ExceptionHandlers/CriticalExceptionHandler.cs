using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.WebAPI.ExceptionHandlers
{
    public class CriticalExceptionHandler() : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //if (exception is CriticalException)
            //{
            //    Console.WriteLine($"Critical exception: {exception.Message}, Mail has been sent.");
            //}

            return ValueTask.FromResult(false);
        }
    }
}
