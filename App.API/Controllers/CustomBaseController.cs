using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
        {
            return serviceResult.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created => Created(serviceResult.UrlAsCreated, serviceResult),
                _ => new ObjectResult(serviceResult)
                {
                    StatusCode = serviceResult.StatusCode.GetHashCode()
                }
            };
        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult serviceResult)
        {
            return serviceResult.StatusCode switch
            {
                HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() },
                _ => new ObjectResult(serviceResult)
                {
                    StatusCode = serviceResult.StatusCode.GetHashCode()
                }
            };
        }
    }
}
