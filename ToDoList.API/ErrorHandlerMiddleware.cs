using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ToDoList.API
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private Logger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, Logger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AuthenticationException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.Error(error, error.Message);

                await response.WriteAsync("An error occured.");
            }
        }
    }
}
