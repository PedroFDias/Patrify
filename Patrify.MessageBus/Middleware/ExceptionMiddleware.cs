using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Patrify.MessageBus.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                });

                var response = new
                {
                    Success = false,
                    Message = "Erro de validação",
                    Errors = errors
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
