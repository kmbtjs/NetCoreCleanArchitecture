﻿using App.Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace App.WebAPI.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorAsDto = ServiceResult.Failure(exception.Message, HttpStatusCode.InternalServerError);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

            return true;
        }
    }
}
