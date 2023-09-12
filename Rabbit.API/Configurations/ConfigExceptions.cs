using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rabbit.API.Services.Exceptions;

namespace Rabbit.API.Configurations
{
    public class ConfigExceptions : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { Error = context.Exception.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
