using Bookstore.BusinessLogic.Exceptions;
using System.Text.Json;

namespace Bookstore.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                var statusCode = GetStatusCode(e);
                var response = context.Response;
                response.StatusCode = statusCode;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { errors = e?.Message });
                await response.WriteAsync(result);
            }
        }

        private int GetStatusCode(Exception error) => error switch
        {
            PublisherNotFoundException => StatusCodes.Status404NotFound,
            PublisherHasBooksException => StatusCodes.Status400BadRequest,
            NotUniquePublisherException => StatusCodes.Status400BadRequest,
            BookNotFoundException => StatusCodes.Status404NotFound,
            NotUniqueBookException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
